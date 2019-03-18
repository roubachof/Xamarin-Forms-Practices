using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Sharpnado.Infrastructure.Services;
using Sharpnado.Presentation.Forms.Paging;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Localization;
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

        public GridPageViewModel(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;

            InitCommands();

            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            SillyPeoplePaginator = new Paginator<SillyDude>(LoadSillyPeoplePageAsync, pageSize: PageSize);
            SillyPeopleLoader = new ViewModelLoader<IReadOnlyCollection<SillyDude>>(
                ApplicationExceptions.ToString,
                SillyResources.Empty_Screen);
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetAndRaise(ref _currentIndex, value);
        }

        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public ICommand OnScrollBeginCommand { get; private set; }

        public ICommand OnScrollEndCommand { get; private set; }

        public ViewModelLoader<IReadOnlyCollection<SillyDude>> SillyPeopleLoader { get; }

        public Paginator<SillyDude> SillyPeoplePaginator { get; }

        public ObservableRangeCollection<SillyDudeVmo> SillyPeople
        {
            get => _sillyPeople;
            set => SetAndRaise(ref _sillyPeople, value);
        }

        public override void Load(object parameter)
        {
            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();

            SillyPeopleLoader.Load(async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
        }

        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(
                parameter => NavigationService.NavigateToAsync<SillyDudeVm>(((SillyDudeVmo)parameter).Id));

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
            // NotifyTask.Create(
            //    async () =>
            //    {
            //        await Task.Delay(2000);
            //        CurrentIndex = 15;
            //    });

            return resultPage;
        }
    }
}