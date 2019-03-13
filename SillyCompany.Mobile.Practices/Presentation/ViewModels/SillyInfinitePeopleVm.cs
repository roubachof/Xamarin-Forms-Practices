// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyPeopleScreenVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Sharpnado.Infrastructure.Services;
using Sharpnado.Infrastructure.Tasks;
using Sharpnado.Presentation.Forms.Paging;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Localization;
using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    public class SillyInfinitePeopleVm : ANavigableViewModel
    {
        private const int PageSize = 10;
        private readonly ISillyDudeService _sillyDudeService;

        private int _currentIndex = -1;

        public SillyInfinitePeopleVm(INavigationService navigationService, ISillyDudeService sillyDudeService, ErrorEmulator errorEmulator)
            : base(navigationService)
        {
            _sillyDudeService = sillyDudeService;
            InitCommands();

            ErrorEmulator = new ErrorEmulatorVm(errorEmulator, Load);

            SillyPeople = new ObservableRangeCollection<SillyDudeVmo>();
            SillyPeoplePaginator = new Paginator<SillyDude>(LoadSillyPeoplePageAsync, pageSize: PageSize, loadingThreshold: 0.1f);
            SillyPeopleLoader = new ViewModelLoader<IReadOnlyCollection<SillyDude>>(
                ApplicationExceptions.ToString,
                SillyResources.Empty_Screen);
        }

        public int CurrentIndex
        {
            get => _currentIndex;
            set => SetAndRaise(ref _currentIndex, value);
        }

        public ErrorEmulatorVm ErrorEmulator { get; }

        public SillyDudeVmo SillyOfTheDay { get; private set; }

        public ViewModelLoader<IReadOnlyCollection<SillyDude>> SillyPeopleLoader { get; }

        public Paginator<SillyDude> SillyPeoplePaginator { get; }

        public ObservableRangeCollection<SillyDudeVmo> SillyPeople { get; set; }

        /// <summary>
        /// Commands accessible directly on screen are declared in the ScreenVm.
        /// Here, it is a command to navigate to the second screen.
        /// </summary>
        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        public IAsyncCommand GoToSillyInfiniteGridPeopleCommand { get; protected set; }

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

            SillyPeopleLoader.Load(async () =>
            {
                SillyOfTheDay = new SillyDudeVmo(await _sillyDudeService.GetRandomSilly(), GoToSillyDudeCommand);
                RaisePropertyChanged(nameof(SillyOfTheDay));

                return (await SillyPeoplePaginator.LoadPage(1)).Items;
            });
        }

        /// <summary>
        /// We usually init all commands in a "InitCommands" method which is called in constructor.
        /// </summary>
        private void InitCommands()
        {
            GoToSillyDudeCommand = AsyncCommand.Create(parameter => GoToSillyDudeAsync((SillyDudeVmo)parameter));
            GoToSillyInfiniteGridPeopleCommand = AsyncCommand.Create(GoToSillyInfiniteGridPeopleAsync);

            OnScrollBeginCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SillyInfinitePeopleVm: OnScrollBeginCommand"));
            OnScrollEndCommand = new Command(
                () => System.Diagnostics.Debug.WriteLine("SillyInfinitePeopleVm: OnScrollEndCommand"));
        }

        private async Task<PageResult<SillyDude>> LoadSillyPeoplePageAsync(int pageNumber, int pageSize, bool isRefreshed)
        {
            PageResult<SillyDude> resultPage = await _sillyDudeService.GetSillyPeoplePage(pageNumber, pageSize);
            var viewModels = resultPage.Items.Select(dude => new SillyDudeVmo(dude, GoToSillyDudeCommand)).ToList();

            SillyPeople.AddRange(viewModels);

            // Uncomment to test CurrentIndex property
            //NotifyTask.Create(
            //   async () =>
            //   {
            //       await Task.Delay(2000);
            //       CurrentIndex = 5;
            //   });

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

            await NavigationService.NavigateToAsync<SillyDudeVm>(sillyDude.Id);
        }

        private Task GoToSillyInfiniteGridPeopleAsync()
        {
            return NavigationService.NavigateToAsync<SillyInfiniteGridPeopleVm>();
        }
    }
}