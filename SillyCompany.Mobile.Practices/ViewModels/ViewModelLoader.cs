using System;
using System.Threading.Tasks;
using System.Windows.Input;
using MetroLog;
using SillyCompany.Mobile.Practices.NotifyTask;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.ViewModels
{
    public interface IViewModelLoader
    {
        ICommand ReloadCommand { get; }

        ICommand RefreshCommand { get; }

        bool IsCompleted { get; }

        bool IsNotStarted { get; }

        bool IsNotCompleted { get; }

        bool IsSuccessfullyCompleted { get; }

        bool IsCanceled { get; }

        bool IsFaulted { get; }

        string ErrorMessage { get; }
    }

    public class ViewModelLoader<TData> : BindableObject, IViewModelLoader
    {
        protected static readonly ILogger Log = LoggerFactory.GetLogger("ViewModelLoader");

        private readonly object _syncRoot = new object();

        private Func<Task<TData>> _loadingTaskSource;

        private INotifyTask<TData> _currentLoadingTask = NotifyTask<TData>.NotStartedTask;

        private bool _showLoader;
        private bool _showRefresher;
        private bool _showResult;
        private bool _showError;
        private bool _showErrorNotification;

        private TData _result;
        private string _errorMessage;

        public ViewModelLoader()
        {
            ReloadCommand = new Command(() => Load(_loadingTaskSource));
            RefreshCommand = new Command(() => Load(_loadingTaskSource, isRefreshing: true));
        }

        public ICommand ReloadCommand { get; protected set; }

        public ICommand RefreshCommand { get; protected set; }
        
        public bool IsCompleted => _currentLoadingTask.IsCompleted;

        public bool IsNotStarted => _currentLoadingTask == null || _currentLoadingTask.IsNotStarted;

        public bool IsNotCompleted => _currentLoadingTask.IsNotCompleted;

        public bool IsSuccessfullyCompleted => _currentLoadingTask.IsSuccessfullyCompleted;

        public bool IsCanceled => _currentLoadingTask.IsCanceled;

        public bool IsFaulted => _currentLoadingTask.IsFaulted;

        public bool ShowLoader
        {
            get => _showLoader;
            set => SetAndRaise(ref _showLoader, value);
        }

        public bool ShowRefresher
        {
            get => _showRefresher;
            set => SetAndRaise(ref _showRefresher, value);
        }

        public bool ShowResult
        {
            get => _showResult;
            set => SetAndRaise(ref _showResult, value);
        }

        public bool ShowError
        {
            get => _showError;
            set => SetAndRaise(ref _showError, value);
        }

        public bool ShowErrorNotification
        {
            get => _showErrorNotification;
            set => SetAndRaise(ref _showErrorNotification, value);
        }

        public TData Result
        {
            get => _result;
            set => SetAndRaise(ref _result, value);
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetAndRaise(ref _errorMessage, value);
        }

        public void Load(Func<Task<TData>> loadingTaskSource, bool isRefreshing = false)
        {
            Log.Info("Load");

            lock (_syncRoot)
            {
                if (_currentLoadingTask != NotifyTask<TData>.NotStartedTask && _currentLoadingTask.IsNotCompleted)
                {
                    Log.Warn("A loading task is currently running: discarding this call");
                    return;
                }

                _loadingTaskSource = loadingTaskSource;

                _currentLoadingTask = null;
                _currentLoadingTask = new NotifyTask<TData>.Builder(_loadingTaskSource)
                    .WithWhenCompleted(
                        completedTask =>
                        {
                            Log.Info("Task completed");

                            RaisePropertyChanged(nameof(IsCompleted));
                            RaisePropertyChanged(nameof(IsNotCompleted));
                            RaisePropertyChanged(nameof(IsNotStarted));

                            ShowRefresher = ShowLoader = false;
                        })
                    .WithWhenSuccessfullyCompleted(
                        (completedTask, result) =>
                        {
                            Log.Info("Task successfully completed");
                            RaisePropertyChanged(nameof(IsSuccessfullyCompleted));

                            ShowResult = true;
                            Result = result;
                        })
                    .WithWhenFaulted(
                        faultedTask =>
                        {
                            Log.Info("Task completed with fault");

                            RaisePropertyChanged(nameof(IsFaulted));

                            ShowError = !isRefreshing;
                            ShowErrorNotification = isRefreshing;
                            ErrorMessage = ToErrorMessage(faultedTask.InnerException);
                        })
                    .Build();

                _currentLoadingTask.Start();
            }

            Reset(isRefreshing);
        }

        public string ToErrorMessage(Exception exception)
        {
            return $"An unknown error occured.{Environment.NewLine}Please try again later.";
        }

        private void Reset(bool isRefreshing)
        {
            ShowLoader = !isRefreshing;
            ShowRefresher = isRefreshing;

            if (!isRefreshing)
            {
                ShowError = ShowResult = false;
            }
            
            RaisePropertyChanged(nameof(IsCompleted));
            RaisePropertyChanged(nameof(IsNotCompleted));
            RaisePropertyChanged(nameof(IsNotStarted));
            RaisePropertyChanged(nameof(IsSuccessfullyCompleted));
            RaisePropertyChanged(nameof(IsFaulted));
            RaisePropertyChanged(nameof(Result));
        }
    }
}
