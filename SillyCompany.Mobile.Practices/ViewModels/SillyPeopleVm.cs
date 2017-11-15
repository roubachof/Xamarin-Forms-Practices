// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyPeopleScreenVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly people vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

using SillyCompany.Mobile.Practices.Commands;
using SillyCompany.Mobile.Practices.NotifyTask;
using SillyCompany.Mobile.Practices.Services;
using SillyCompany.Mobile.Practices.Services.Navigables;
using SillyCompany.Mobile.Practices.ViewModels.ViewModelObjects;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.ViewModels
{
    public class SillyPeopleVm : ANavigableViewModel
    {
        private readonly ISillyFrontService _sillyFrontService;
        
        private bool _isFirstLoadNotCompleted;

        private bool _isRefreshing;
        
        public SillyPeopleVm(INavigationService navigationService, ISillyFrontService sillyFrontService)
            : base(navigationService)
        {
            _sillyFrontService = sillyFrontService;
            InitCommands();
            _isFirstLoadNotCompleted = true;
        }

        /// <summary>
        /// Gets or sets the silly people task.
        /// </summary>
        public NotifyTask<IReadOnlyList<SillyVmo>> SillyPeopleTask { get; set; }

        /// <summary>
        /// Gets a value indicating whether [first load not completed].
        /// </summary>
        /// <value><c>true</c> if [first load not completed]; otherwise, <c>false</c>.</value>
        public bool IsFirstLoadNotCompleted
        {
            get => _isFirstLoadNotCompleted;
            set => SetAndRaise(ref _isFirstLoadNotCompleted, value);
        }

        public bool IsRefreshing
        {
            get => _isRefreshing;
            set => SetAndRaise(ref _isRefreshing, value);
        }

        /// <summary>
        /// Gets or sets the reload command.
        /// </summary>
        public ICommand ReloadCommand { get; protected set; }

        public ICommand RefreshCommand { get; protected set; }

        /// <summary>
        /// Commands accessible directly on screen are declared in the ScreenVm.
        /// Here, it is a command to navigate to the second screen.
        /// </summary>
        public IAsyncCommand GoToSillyDudeCommand { get; protected set; }

        /// <summary>
        /// We usually init all commands in a "InitCommands" method which is called in constructor
        /// </summary>
        private void InitCommands()
        {
            ReloadCommand = new Command(() => Load(null));
            RefreshCommand = new Command(() =>
            {
                IsRefreshing = true;
                Load(null);
            });

            GoToSillyDudeCommand = AsyncCommand.Create(parameter => GoToSillyDude((SillyVmo)parameter));
        }

        /// <summary>
        /// Loads the specified parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public override void Load(object parameter)
        {
            SillyPeopleTask = NotifyTask<IReadOnlyList<SillyVmo>>.Create(
                LoadSillyPeople(), 
                whenCompleted: task => IsRefreshing = false);

            RaisePropertyChanged(() => this.SillyPeopleTask);
        }

        /// <summary>
        /// The load silly people.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        private async Task<IReadOnlyList<SillyVmo>> LoadSillyPeople()
        {
            try
            {
                return await _sillyFrontService.GetSillyPeople();
            }
            finally
            {
                IsFirstLoadNotCompleted = false;
            }
        }

        /// <summary>
        /// The go to silly dude.
        /// </summary>
        /// <param name="sillyDude">The silly dude.</param>
        /// <returns>The <see cref="Task" />.</returns>
        /// <exception cref="System.InvalidOperationException">The knigths demand...... A SACRIFICE!</exception>
        private async Task GoToSillyDude(SillyVmo sillyDude)
        {
            if (sillyDude.Id == 2)
            {
                throw new InvalidOperationException("The knigths demand...... A SACRIFICE!");
            }

            await NavigationService.NavigateToAsync<SillyDudeVm>(sillyDude.Id);
        }
    }
}