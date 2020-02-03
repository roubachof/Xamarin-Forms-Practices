using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

using Sharpnado.Presentation.Forms;
using Sharpnado.Presentation.Forms.Paging;
using Sharpnado.Presentation.Forms.Services;
using Sharpnado.Presentation.Forms.ViewModels;

using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout
{
    public class ListPageViewModel : ANavigableViewModel
    {
        private const int PageSize = 10;
        private readonly ISillyDudeService _sillyDudeService;

        private int _currentIndex = -1;

        public ListPageViewModel(
            INavigationService navigationService,
            ISillyDudeService sillyDudeService,
            ErrorEmulator errorEmulator)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;
            InitCommands();

            ErrorEmulator = new ErrorEmulatorVm(errorEmulator, Load);

            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            SillyPeoplePaginator = new Paginator<SillyDude>(
                LoadSillyPeoplePageAsync,
                pageSize: PageSize,
                loadingThreshold: 0.1f);
            SillyPeopleLoaderNotifier = new TaskLoaderNotifier<IReadOnlyCollection<SillyDude>>();
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetAndRaise(ref _currentIndex, value);
        }

        public ErrorEmulatorVm ErrorEmulator { get; }

        public TaskLoaderNotifier<IReadOnlyCollection<SillyDude>> SillyPeopleLoaderNotifier { get; }

        public Paginator<SillyDude> SillyPeoplePaginator { get; }

        public ObservableRangeCollection<SillyDudeVmo> SillyPeople { get; set; }

        /// <summary>
        /// Commands accessible directly on screen are declared in the ScreenVm.
        /// Here, it is a command to navigate to the second screen.
        /// </summary>
        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public ICommand OnScrollBeginCommand { get; private set; }

        public ICommand OnScrollEndCommand { get; private set; }

        public override void Load(object parameter)
        {
            Load();
        }

        private void Load()
        {
            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            RaisePropertyChanged(nameof(SillyPeople));

            SillyPeopleLoaderNotifier.Load(
                async () => (await SillyPeoplePaginator.LoadPage(1)).Items);
        }

        /// <summary>
        /// We usually init all commands in a "InitCommands" method which is called in constructor.
        /// </summary>
        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(parameter => GoToSillyDudeAsync((SillyDudeVmo)parameter));

            OnScrollBeginCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("ListPageViewModel: OnScrollBeginCommand"));
            OnScrollEndCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("ListPageViewModel: OnScrollEndCommand"));
        }

        private async Task<PageResult<SillyDude>> LoadSillyPeoplePageAsync(int pageNumber, int pageSize, bool isRefresh)
        {
            PageResult<SillyDude> resultPage = await _sillyDudeService.GetSillyPeoplePage(pageNumber, pageSize);
            var viewModels = resultPage.Items.Select(dude => new SillyDudeVmo(dude, GoToSillyDudeCommand)).ToList();

            SillyPeople.AddRange(viewModels);

            // Uncomment to test CurrentIndex property
            //TaskMonitor.Create(
            //   async () =>
            //   {
            //       await Task.Delay(5000);
            //       CurrentIndex = 5;
            //   });

            // Uncomment to test ItemsSource changed
            //TaskMonitor.Create(
            //    async () =>
            //    {
            //        await Task.Delay(10000);
            //        SillyPeople = new ObservableRangeCollection<SillyDudeVmo>(viewModels);
            //        RaisePropertyChanged(nameof(SillyPeople));
            //    });

            return resultPage;
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

            if (PlatformService.IsFoldingScreen)
            {
                return;
            }

            await NavigationService.NavigateToAsync<SillyDudeVm>(sillyDude.Id);
        }
    }
}