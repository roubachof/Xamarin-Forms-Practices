using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Sharpnado.Infrastructure.Tasks;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Localization;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.ViewModelObjects;

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
