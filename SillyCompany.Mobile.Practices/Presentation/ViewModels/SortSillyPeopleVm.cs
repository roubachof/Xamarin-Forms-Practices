using System.Collections.ObjectModel;
using System.Windows.Input;
using Sharpnado.Infrastructure.Tasks;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    public class SortSillyPeopleVm : ANavigableViewModel
    {
        public SortSillyPeopleVm(INavigationService navigationService)
            : base(navigationService)
        {
            InitCommands();
        }

        public ICommand OnDragAndDropEndCommand { get; private set; }

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

        private void InitCommands()
        {
            OnDragAndDropEndCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SortSillyPeopleVm: OnDragAndDropEndCommand"));
        }
    }
}
