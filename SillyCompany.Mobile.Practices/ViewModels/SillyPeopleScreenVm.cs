// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyPeopleScreenVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly people vm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices.ComTypes;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using SillyCompany.Mobile.Practices.AsyncCommand;
    using SillyCompany.Mobile.Practices.NotifyTask;
    using SillyCompany.Mobile.Practices.Services;
    using SillyCompany.Mobile.Practices.Services.Navigables;
    using SillyCompany.Mobile.Practices.ViewModels.ViewModelObjects;

    using Xamarin.Forms;

    /// <summary>
    /// The silly people vm.
    /// </summary>
    public class SillyPeopleVm : ANavigableViewModel
    {
        /// <summary>
        /// The silly front service.
        /// </summary>
        private readonly ISillyFrontService sillyFrontService;

        /// <summary>
        /// The is first load not completed
        /// </summary>
        private bool isFirstLoadNotCompleted;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyPeopleVm"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        /// <param name="sillyFrontService">
        /// The silly front service.
        /// </param>
        public SillyPeopleVm(INavigationService navigationService, ISillyFrontService sillyFrontService)
            : base(navigationService)
        {
            this.sillyFrontService = sillyFrontService;
            this.InitCommands();
            this.isFirstLoadNotCompleted = true;
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
            get { return this.isFirstLoadNotCompleted; }
            set { this.SetAndRaise(ref this.isFirstLoadNotCompleted, value); }
        }

        /// <summary>
        /// Gets or sets the reload command.
        /// </summary>
        public ICommand ReloadCommand { get; protected set; }

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
            this.ReloadCommand = new Command(() => this.Load(null));
            this.GoToSillyDudeCommand = AsyncCommand.Create(parameter => this.GoToSillyDude((SillyVmo)parameter));
        }

        /// <summary>
        /// Loads the specified parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public override void Load(object parameter)
        {
            this.SillyPeopleTask = NotifyTask<IReadOnlyList<SillyVmo>>.Create(this.LoadSillyPeople());
            this.RaisePropertyChanged(() => this.SillyPeopleTask);
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
                return await this.sillyFrontService.GetSillyPeople();
            }
            finally
            {
                this.IsFirstLoadNotCompleted = false;
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

            await this.NavigationService.NavigateToAsync<SillyDudeVm>(sillyDude.Id);
        }
    }
}