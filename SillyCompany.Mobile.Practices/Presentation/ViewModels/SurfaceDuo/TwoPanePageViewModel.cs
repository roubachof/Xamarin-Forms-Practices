using System.Linq;

using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.SurfaceDuo
{
    public class TwoPanePageViewModel : ANavigableViewModel
    {
        private int? _selectedSillyDudeId;

        public TwoPanePageViewModel(
            INavigationService navigationService,
            ISillyDudeService sillyDudeService,
            ErrorEmulator errorEmulator)
            : base(navigationService)
        {
            SillyBottomTabsPageViewModel = new SillyBottomTabsPageViewModel(
                navigationService,
                sillyDudeService,
                errorEmulator);

            SillyDudeViewModel = new SillyDudeVm(navigationService, sillyDudeService);
        }

        public SillyBottomTabsPageViewModel SillyBottomTabsPageViewModel { get; }

        public SillyDudeVm SillyDudeViewModel { get; }

        public override void Load(object parameter)
        {
            SillyBottomTabsPageViewModel.Load(null);
            SillyBottomTabsPageViewModel.HomePageViewModel.SillyDudeLoaderNotifier.PropertyChanged += SubViewModelsPropertyChanged;
            SillyBottomTabsPageViewModel.ListPageViewModel.PropertyChanged += SubViewModelsPropertyChanged;
            SillyBottomTabsPageViewModel.GridPageViewModel.PropertyChanged += SubViewModelsPropertyChanged;
        }

        private void SubViewModelsPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Result")
            {
                _selectedSillyDudeId =
                    SillyBottomTabsPageViewModel.HomePageViewModel.SillyDudeLoaderNotifier.Result?.Id;
                OnSillyDudeIdChanged();
            }

            if (e.PropertyName == nameof(ListPageViewModel.CurrentIndex) && sender is ListPageViewModel listPageViewModel)
            {
                _selectedSillyDudeId =
                    listPageViewModel.SillyPeople.ElementAt(listPageViewModel.CurrentIndex).Id;
                OnSillyDudeIdChanged();
            }

            if (e.PropertyName == nameof(GridPageViewModel.SelectedDudeId))
            {
                _selectedSillyDudeId = SillyBottomTabsPageViewModel.GridPageViewModel.SelectedDudeId;
                OnSillyDudeIdChanged();
            }
        }

        private void OnSillyDudeIdChanged()
        {
            SillyDudeViewModel.Load(_selectedSillyDudeId);
        }
    }
}
