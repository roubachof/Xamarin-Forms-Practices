using System.Collections.ObjectModel;
using Sharpnado.Infrastructure.Tasks;
using SillyCompany.Mobile.Practices.Presentation.Navigables;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    public class SortSillyPeopleVm : ANavigableViewModel
    {
        public SortSillyPeopleVm(INavigationService navigationService)
            : base(navigationService)
        {
        }

        public ObservableCollection<SillyDudeVmo> SillyPeople { get; set; }

        public override void Load(object parameter)
        {
            if (parameter is ObservableCollection<SillyDudeVmo> observableDudes)
            {
                SillyPeople = observableDudes;
                RaisePropertyChanged(nameof(SillyPeople));
                return;
            }

            NotifyTask.Create(NavigationService.NavigateBackAsync(typeof(SortSillyPeopleVm)));
        }
    }
}
