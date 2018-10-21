using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SillyCompany.Mobile.Practices.Presentation
{
    /// <summary>
    /// Interface IInfiniteListLoader.
    /// </summary>
    public interface IInfiniteListLoader
    {
        /// <summary>
        /// This method must be called by the UI element in charge of displaying data.
        /// Per example, on android, a scroll listener can reference IInfiniteListLoader and call it from OnScroll.
        /// The implementation execution time of this method must be transparent as it should return immediately and doesn't block the caller.
        /// </summary>
        /// <param name="lastVisibleIndex">Index of the last visible item.</param>
        void OnScroll(int lastVisibleIndex);
    }

    public struct PageResult<TItem>
    {
        public static readonly PageResult<TItem> Empty = new PageResult<TItem>(0, new List<TItem>());

        public PageResult(int totalCount, IReadOnlyList<TItem> items)
        {
            TotalCount = totalCount;
            Items = items ?? new List<TItem>();
        }

        public int TotalCount { get; }

        public IReadOnlyList<TItem> Items { get; }

        public static bool operator ==(PageResult<TItem> p1, PageResult<TItem> p2)
        {
            return ReferenceEquals(p1.Items, p2.Items) && p1.TotalCount == p2.TotalCount;
        }

        public static bool operator !=(PageResult<TItem> p1, PageResult<TItem> p2)
        {
            return !ReferenceEquals(p1.Items, p2.Items) || p1.TotalCount != p2.TotalCount;
        }

        public bool Equals(PageResult<TItem> other)
        {
            return TotalCount == other.TotalCount && Equals(Items, other.Items);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            return obj is PageResult<TItem> result && Equals(result);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (TotalCount * 397) ^ (Items != null ? Items.GetHashCode() : 0);
            }
        }
    }

    /// <summary>
    /// Component responsible for manually loading the pages of a service.
    /// It also bears the responsibility of loading the next page when scrolling the infinite list.
    /// This automatic loading feature is exposed by the IInfiniteListLoader interface.
    /// And yes: it has 2 responsibilities, which goes against SRP, but it makes the component very efficient, simple and usable.
    /// So *** SRP (well not generally speaking, but for this one time).
    /// </summary>
    public class Paginator<TResult> : IInfiniteListLoader, IDisposable
    {
        private const float LoadingThresholdDefault = 1f / 4;
        private const int PageSizeDefault = 10;
        private const int MaxItemCountDefault = 200;

        private readonly object _syncRoot = new object();
        private readonly int _maxItemCount;
        private readonly Func<int, int, Task<PageResult<TResult>>> _pageSourceLoader;
        private readonly Action<Task> _onTaskCompleted;
        private readonly float _loadingThreshold;

        private bool _isDisposed;
        private List<TResult> _items;
        private bool _refreshRequested;
        private CancellationTokenSource _loadingTaskTokenSource;

        /// <summary>
        /// The paginator is a concrete component, it is usable directly by instantiation (please don't abuse DI :).
        /// </summary>
        /// <param name="pageSize">The page size for the data.</param>
        /// <param name="maxItemCount">The maximum number of elements that the paginator can load.</param>
        /// <param name="pageSourceLoader">
        /// The func that will return the data. Here you have two options:
        /// 1. The func calls the REST (or whatever) service, build ViewModels from the Models, then add them to collection the ObservableCollection.
        /// The onTaskCompleted callback will be optional.
        /// 2. The func still calls the domain service, but just returns the PageResult of Models.
        /// The onTaskCompleted will create your ViewModels and add them to the ObservableCollection.
        /// The two parameters of the Func are pageNumber and pageSize.
        /// </param>
        /// <param name="onTaskCompleted">
        /// This callback is called at the end of each page loading, successful or not.
        /// This is where you want to assign the retrieved items to your ObservableCollection.
        /// </param>
        /// <param name="loadingThreshold">
        /// The list threshold from where the next page loading will be triggered (magic will occur in the OnScroll method of the IInfiniteListLoader interface)
        /// This threshold stands for a percentage of the last page:
        /// Let's say you have 40 items loaded in your List and page size is 10, if the threshold is 0.5,
        /// the loading of the next page will be triggered when element 35 will become visible.
        /// Default is 0.25. Requires loadingThreshold in [0,1].
        /// </param>
        public Paginator(
            Func<int, int, Task<PageResult<TResult>>> pageSourceLoader,
            Action<Task> onTaskCompleted = null,
            int pageSize = PageSizeDefault,
            int maxItemCount = MaxItemCountDefault,
            float loadingThreshold = LoadingThresholdDefault)
        {
            Debug.Assert(pageSize > 0);
            Debug.Assert(maxItemCount > 0);
            Debug.Assert(loadingThreshold >= 0 && loadingThreshold <= 1);

            Debug.WriteLine($"Building paginator with pageSize: {pageSize}, maxItemCount: {maxItemCount}, loadingThreshold: {loadingThreshold}");

            _maxItemCount = maxItemCount;
            _pageSourceLoader = pageSourceLoader;
            _onTaskCompleted = onTaskCompleted;
            _loadingThreshold = loadingThreshold;

            PageSize = pageSize;
            TotalCount = _maxItemCount;

            Reset();
        }

        public Task<PageResult<TResult>> LoadingTask { get; private set; }

        /// <summary>
        /// Number of pages successfully loaded.
        /// </summary>
        public int PageLoadedCount { get; private set; }

        /// <summary>
        /// Number of items successfully loaded.
        /// </summary>
        public int LoadedCount => Items.Count;

        public bool IsFull => LoadedCount >= TotalCount;

        public int PageSize { get; }

        public int TotalCount { get; private set; }

        public int TotalRemoteCount { get; private set; }

        public bool HasStarted => LoadingTask != null;

        public bool IsLoadingSuccessfull => LoadingTask.Status == TaskStatus.RanToCompletion;

        /// <summary>
        /// True if the user requested a refresh of the list.
        /// </summary>
        public bool HasRefreshed
        {
            get
            {
                lock (_syncRoot)
                {
                    return _refreshRequested;
                }
            }
        }

        public IReadOnlyList<TResult> Items => _items;

        /// <summary>
        /// Last page returned by the data source.
        /// </summary>
        public PageResult<TResult> LastResult { get; private set; }

        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;
        }

        /// <summary>
        /// This method must be called by the UI element in charge of displaying data.
        /// Per example, on android, a scroll listener can reference this paginator as an IInfiniteListLoader and call it from OnScroll.
        /// The call to this method is nearly transparent as it returns immediately and doesn't block the caller.
        /// (benchmarked as 4 ticks for a call (10 000 ticks == 1ms)).
        /// </summary>
        public async void OnScroll(int lastVisibleIndex)
        {
            try
            {
                if (await ShouldLoadNextPage(lastVisibleIndex))
                {
                    Debug.WriteLine($"Scrolled: loading more (max index of visible item {lastVisibleIndex})");
                    int pageToLoad = lastVisibleIndex / PageSize + 2;
                    await LoadPage(pageToLoad, calledFromScroll: true);
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine($"Error in OnScroll task: {exception}");
            }
        }

        /// <summary>
        /// Launch the loading of a data page.
        /// If a task is currently running, it gets discarded (callbacks won't be called).
        /// If the first page loading is asked whereas one or several pages have already been loaded, a "refresh" is detected.
        /// </summary>
        /// <param name="pageNumber">The page number to load (pageNumber = 1 for the first page).</param>
        /// <param name="calledFromScroll">True if LoadPage has been called from OnScroll method of the IInfiniteListLoader.</param>
        public Task<PageResult<TResult>> LoadPage(int pageNumber, bool calledFromScroll = false)
        {
            Debug.Assert(pageNumber > 0);
            Debug.Assert(
                calledFromScroll || pageNumber == 1 || pageNumber == (PageLoadedCount + 1),
                "The paginator can only load sequential pages");

            Debug.WriteLine($"Requesting page n°{pageNumber} load, {PageLoadedCount} pages loaded so far");
            lock (_syncRoot)
            {
                if (calledFromScroll)
                {
                    if (pageNumber <= PageLoadedCount)
                    {
                        Debug.WriteLine(
                            $"Aborting IInfiniteListLoader call: only a direct call to LoadPage can lead to a refresh");
                        return Task.FromResult(PageResult<TResult>.Empty);
                    }
                }

                if (pageNumber > PageLoadedCount && IsFull)
                {
                    Debug.WriteLine(
                        $"Cannot load page {pageNumber} total item count has already been reached ({TotalCount})");
                    return Task.FromResult(PageResult<TResult>.Empty);
                }

                if (pageNumber == 1 && PageLoadedCount > 0)
                {
                    Debug.WriteLine("Refresh detected");
                    _refreshRequested = true;
                }
                else
                {
                    _refreshRequested = false;
                }

                if (LoadingTask != null && !LoadingTask.IsCompleted)
                {
                    // Cancels callbacks of previous task if not completed
                    _loadingTaskTokenSource.Cancel();
                }

                _loadingTaskTokenSource = new CancellationTokenSource();
                LoadingTask = _pageSourceLoader(pageNumber, PageSize);

                MonitorLoadingTask(_loadingTaskTokenSource.Token);
            }

            return LoadingTask;
        }

        private void MonitorLoadingTask(CancellationToken cancellationToken)
        {
            Task.Run(
                    async () =>
                    {
                        Debug.WriteLine("MonitorLoadingTask");
                        try
                        {
                            await LoadingTask;
                            if (cancellationToken.IsCancellationRequested)
                            {
                                Debug.WriteLine("Loading task has been canceled by user");
                                return;
                            }

                            OnPageRetrieved(LoadingTask);
                        }
                        catch (TaskCanceledException canceledException)
                        {
                            Debug.WriteLine($"Task has been canceled {canceledException}");
                        }
                        catch (Exception exception)
                        {
                            Debug.WriteLine($"Error in wrapped task {exception}");
                        }
                    })
                .ContinueWith(task => OnTaskCompleted(LoadingTask), TaskScheduler.FromCurrentSynchronizationContext());
        }

        private void OnPageRetrieved(Task<PageResult<TResult>> task)
        {
            Debug.WriteLine("OnPageRetrieved()");

            if (_isDisposed)
            {
                return;
            }

            var result = task.Result;
            lock (_syncRoot)
            {
                LastResult = result;

                Debug.WriteLine($"{result.Items.Count} items retrieved, total remote items is {result.TotalCount}");
                if (_refreshRequested)
                {
                    Reset();
                }

                TotalRemoteCount = result.TotalCount;
                TotalCount = Math.Min(result.TotalCount, _maxItemCount);
                PageLoadedCount++;
            }

            _items.AddRange(result.Items);
            Debug.WriteLine($"{Items.Count} items in paginator collection, {PageLoadedCount} pages loaded");

            Debug.Assert(PageLoadedCount > 0);
            Debug.Assert(result.Items != null && _maxItemCount >= 0);
        }

        private void OnTaskCompleted(Task task)
        {
            Debug.WriteLine($"OnTaskCompleted( taskStatus: {task.Status} )");
            if (_isDisposed)
            {
                return;
            }

            _onTaskCompleted?.Invoke(task);
        }

        private void Reset()
        {
            Debug.WriteLine("Resetting paginator");
            PageLoadedCount = 0;
            _items = new List<TResult>();
        }

        private Task<bool> ShouldLoadNextPage(int lastVisibleIndex)
        {
            return Task.Run(() =>
            {
                if (lastVisibleIndex < 0)
                {
                    return false;
                }

                if (PageLoadedCount == 0)
                {
                    // If no pages are loaded, there is nothing to scroll
                    return false;
                }

                if (IsFull)
                {
                    // All messages are already loaded nothing to paginate
                    return false;
                }

                if (HasStarted && !LoadingTask.IsCompleted)
                {
                    // Currently loading page
                    return false;
                }

                int itemsCount = LoadedCount;
                return lastVisibleIndex >= (itemsCount - (PageSize * _loadingThreshold));
            });
        }
    }
}
