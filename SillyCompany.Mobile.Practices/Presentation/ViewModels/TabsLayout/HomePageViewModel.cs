using System;
using System.Threading.Tasks;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout
{
    public class HomePageViewModel : ANavigableViewModel
    {
        private readonly ISillyDudeService _sillyDudeService;

        public HomePageViewModel(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;
            InitCommands();

            SillyDudeLoader = new ViewModelLoader<SillyDudeVmo>();
        }

        public ViewModelLoader<SillyDudeVmo> SillyDudeLoader { get; }

        /// <summary>
        /// Commands accessible directly on screen are declared in the ScreenVm.
        /// Here, it is a command to navigate to the second screen.
        /// </summary>
        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public override void Load(object parameter)
        {
            SillyDudeLoader.Load(InitializationTask);
        }

        /// <summary>
        /// We usually init all commands in a "InitCommands" method which is called in constructor.
        /// </summary>
        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(parameter => GoToSillyDudeAsync((SillyDudeVmo)parameter));
        }

        private async Task<SillyDudeVmo> InitializationTask()
        {
            var dude = await _sillyDudeService.GetRandomSilly();
            return new SillyDudeVmo(dude, GoToSillyDudeCommand);
        }

        /// <param name="sillyDude">The silly dude.</param>
        /// <returns>The <see cref="Task" />.</returns>
        /// <exception cref="System.InvalidOperationException">The knights demand...... A SACRIFICE!</exception>
        private async Task GoToSillyDudeAsync(SillyDudeVmo sillyDude)
        {
            if (sillyDude.Role == "Knights")
            {
                throw new InvalidOperationException("The knights demand...... A SACRIFICE!");
            }

            await NavigationService.NavigateToAsync<SillyDudeVm>(sillyDude.Id);
        }
    }
}