// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyDudeVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   Class SillyDudeVm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System.Threading.Tasks;
using SillyCompany.Mobile.Practices.Services;
using SillyCompany.Mobile.Practices.Services.Navigables;
using SillyCompany.Mobile.Practices.ViewModels.ViewModelObjects;

namespace SillyCompany.Mobile.Practices.ViewModels
{

    /// <summary>
    /// Class SillyDudeVm.
    /// </summary>
    public class SillyDudeVm : ANavigableViewModel
    {
        /// <summary>
        /// The front service.
        /// </summary>
        private readonly ISillyFrontService frontService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyDudeVm"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        /// <param name="sillyFrontService">
        /// The silly front service.
        /// </param>
        public SillyDudeVm(INavigationService navigationService, ISillyFrontService sillyFrontService)
            : base(navigationService)
        {
            this.frontService = sillyFrontService;

            this.SillyDudeLoader = new ViewModelLoader<SillyVmo>();
        }

        /// <summary>
        /// Gets or sets the silly dude task.
        /// </summary>
        /// <value>The silly dude task.</value>
        public ViewModelLoader<SillyVmo> SillyDudeLoader { get; }
        
        /// <summary>
        /// Loads the specified parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public override void Load(object parameter)
        {
            SillyDudeLoader.Load(() => LoadSillyDude((int)parameter));
        }

        private async Task<SillyVmo> LoadSillyDude(int id)
        {
            return await this.frontService.GetSilly(id);
        }
    }
}