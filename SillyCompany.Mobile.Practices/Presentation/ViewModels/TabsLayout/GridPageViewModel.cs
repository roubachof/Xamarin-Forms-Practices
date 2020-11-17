using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Sharpnado.HorizontalListView.Paging;
using Sharpnado.HorizontalListView.Services;
using Sharpnado.HorizontalListView.ViewModels;
using Sharpnado.Presentation.Forms;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout
{
    public class GridPageViewModel : ANavigableViewModel
    {
        private const int PageSize = 20;
        private readonly ISillyDudeService _sillyDudeService;

        private ObservableRangeCollection<SillyDudeVmo> _sillyPeople;

        private int _currentIndex = -1;

        private int? _selectedDudeId;

        public GridPageViewModel(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;

            InitCommands();

            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            SillyPeoplePaginator = new Paginator<SillyDude>(LoadSillyPeoplePageAsync, pageSize: PageSize);
            SillyPeopleLoaderNotifier = new TaskLoaderNotifier<IReadOnlyCollection<SillyDude>>();
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetAndRaise(ref _currentIndex, value);
        }

        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public ICommand OnScrollBeginCommand { get; private set; }

        public ICommand OnScrollEndCommand { get; private set; }

        public TaskLoaderNotifier<IReadOnlyCollection<SillyDude>> SillyPeopleLoaderNotifier { get; }

        public Paginator<SillyDude> SillyPeoplePaginator { get; }

        public ObservableRangeCollection<SillyDudeVmo> SillyPeople
        {
            get => _sillyPeople;
            set => SetAndRaise(ref _sillyPeople, value);
        }

        public int? SelectedDudeId
        {
            get => _selectedDudeId;
            set => SetAndRaise(ref _selectedDudeId, value);
        }

        public override void Load(object parameter)
        {
            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();

            SillyPeopleLoaderNotifier.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
        }

        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(
                parameter =>
                {
                    SelectedDudeId = ((SillyDudeVmo)parameter).Id;
                    if (PlatformService.IsFoldingScreen)
                    {
                        return Task.CompletedTask;
                    }

                    return NavigationService.NavigateToAsync<SillyDudeVm>(((SillyDudeVmo)parameter).Id);
                });

            OnScrollBeginCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SillyInfiniteGridPeopleVm: OnScrollBeginCommand"));
            OnScrollEndCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SillyInfiniteGridPeopleVm: OnScrollEndCommand"));
        }

        private async Task<PageResult<SillyDude>> LoadSillyPeoplePageAsync(int pageNumber, int pageSize, bool isRefresh)
        {
            PageResult<SillyDude> resultPage = await _sillyDudeService.GetSillyPeoplePage(pageNumber, pageSize);
            var viewModels = resultPage.Items.Select(dude => new SillyDudeVmo(dude, GoToSillyDudeCommand)).ToList();

            if (isRefresh)
            {
                SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            }

            SillyPeople.AddRange(viewModels);

            // Uncomment to test CurrentIndex property
            // TaskMonitor.Create(
            //    async () =>
            //    {
            //        await Task.Delay(2000);
            //        CurrentIndex = 15;
            //    });

            // Uncomment to test Reset action
            // TaskMonitor.Create(
            //   async () =>
            //   {
            //       await Task.Delay(6000);
            //       SillyPeople.Clear();
            //       await Task.Delay(3000);
            //       SillyPeople = new ObservableRangeCollection<SillyDudeVmo>(viewModels);
            //   });

            return resultPage;
        }
    }
}