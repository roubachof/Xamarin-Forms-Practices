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
        private readonly ISillyDudeService _sillyDudeService;

        public SortSillyPeopleVm(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;
            SillyPeopleLoader = new ViewModelLoader(ApplicationExceptions.ToString, SillyResources.Empty_Screen);
        }

        public ViewModelLoader SillyPeopleLoader { get; }

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

            // SillyPeopleLoader.Load(LoadSillyPeopleAsync);
        }

        private async Task<IReadOnlyList<SillyDudeVmo>> LoadSillyPeopleAsync()
        {
            return new List<SillyDudeVmo>(
                (await _sillyDudeService.GetSillyPeople())
                .Select(dude => new SillyDudeVmo(dude, null)));
        }
    }
}
