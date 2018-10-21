// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyPeopleScreenVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Localization;
using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.ViewModelObjects;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    public class SillyPeopleVm : ANavigableViewModel
    {
        private readonly ISillyDudeService _sillyDudeService;

        public SillyPeopleVm(INavigationService navigationService, ISillyDudeService sillyDudeService, ErrorEmulator errorEmulator)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;
            InitCommands();

            ErrorEmulator = new ErrorEmulatorVm(errorEmulator, () => SillyPeopleLoader.Load(LoadSillyPeopleAsync));
            SillyPeopleLoader = new ViewModelLoader<ObservableCollection<SillyDudeVmo>>(ApplicationExceptions.ToString, SillyResources.Empty_Screen);
        }

        public ErrorEmulatorVm ErrorEmulator { get; }

        public SillyDudeVmo SillyOfTheDay { get; private set; }

        public ViewModelLoader<ObservableCollection<SillyDudeVmo>> SillyPeopleLoader { get; }

        /// <summary>
        /// Commands accessible directly on screen are declared in the ScreenVm.
        /// Here, it is a command to navigate to the second screen.
        /// </summary>
        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public IAsyncCommand SortSillyPeopleCommand { get; protected set; }

        /// <summary>
        /// Loads the specified parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public override void Load(object parameter)
        {
            if (parameter is SortSillyPeopleVm)
            {
                // Coming from SortSillyPeopleVm
                return;
            }

            SillyPeopleLoader.Load(LoadSillyPeopleAsync);
        }

        /// <summary>
        /// We usually init all commands in a "InitCommands" method which is called in constructor.
        /// </summary>
        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(parameter => GoToSillyDudeAsync((SillyDudeVmo)parameter));
            SortSillyPeopleCommand = AsyncCommand.Create(SortSillyPeopleAsync);
        }

        /// <summary>
        /// The load silly people.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<ObservableCollection<SillyDudeVmo>> LoadSillyPeopleAsync()
        {
            SillyOfTheDay = new SillyDudeVmo(await _sillyDudeService.GetRandomSilly(), GoToSillyDudeCommand);
            RaisePropertyChanged(nameof(SillyOfTheDay));
            return new ObservableCollection<SillyDudeVmo>(
                (await _sillyDudeService.GetSillyPeople())
                    .Select(dude => new SillyDudeVmo(dude, GoToSillyDudeCommand)));
        }

        /// <param name="sillyDude">The silly dude.</param>
        /// <returns>The <see cref="Task" />.</returns>
        /// <exception cref="System.InvalidOperationException">The knigths demand...... A SACRIFICE!</exception>
        private async Task GoToSillyDudeAsync(SillyDudeVmo sillyDude)
        {
            if (sillyDude.Id == 2)
            {
                throw new InvalidOperationException("The knigths demand...... A SACRIFICE!");
            }

            await NavigationService.NavigateToAsync<SillyDudeVm>(sillyDude.Id);
        }

        private Task SortSillyPeopleAsync()
        {
            return NavigationService.NavigateToAsync<SortSillyPeopleVm>(SillyPeopleLoader.Result);
        }
    }
}