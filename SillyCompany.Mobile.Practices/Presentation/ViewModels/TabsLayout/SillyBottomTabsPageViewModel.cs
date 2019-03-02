using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Presentation.Navigables;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout
{
    public class SillyBottomTabsPageViewModel : ANavigableViewModel
    {
        private int _selectedViewModelIndex = 0;

        public SillyBottomTabsPageViewModel(
            INavigationService navigationService,
            ISillyDudeService sillyDudeService,
            ErrorEmulator errorEmulator)
            : base(navigationService)
        {
            HomePageViewModel = new HomePageViewModel(navigationService, sillyDudeService);
            ListPageViewModel = new ListPageViewModel(navigationService, sillyDudeService, errorEmulator);
            GridPageViewModel = new GridPageViewModel(navigationService, sillyDudeService);
        }

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetAndRaise(ref _selectedViewModelIndex, value);
        }

        public HomePageViewModel HomePageViewModel { get; }

        public ListPageViewModel ListPageViewModel { get; }

        public GridPageViewModel GridPageViewModel { get; }

        public override void Load(object parameter)
        {
            HomePageViewModel.Load(parameter);
            ListPageViewModel.Load(parameter);
            GridPageViewModel.Load(parameter);
        }
    }
}
