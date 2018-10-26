// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyDudeVm.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   Class SillyDudeVm.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.Commands;
using Sharpnado.Presentation.Forms.ViewModels;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Presentation.Navigables;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels
{
    /// <summary>
    /// Class SillyDudeVm.
    /// </summary>
    public class SillyDudeVm : ANavigableViewModel
    {
        /// <summary>
        /// The front service.
        /// </summary>
        private readonly ISillyDudeService _dudeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="SillyDudeVm"/> class.
        /// </summary>
        /// <param name="navigationService">
        /// The navigation service.
        /// </param>
        /// <param name="sillyDudeService">
        /// The silly front service.
        /// </param>
        public SillyDudeVm(INavigationService navigationService, ISillyDudeService sillyDudeService)
            : base(navigationService)
        {
            _dudeService = sillyDudeService;

            SillyDudeLoader = new ViewModelLoader<SillyDudeVmo>();
        }

        /// <summary>
        /// Gets or sets the silly dude task.
        /// </summary>
        /// <value>The silly dude task.</value>
        public ViewModelLoader<SillyDudeVmo> SillyDudeLoader { get; }

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

        private async Task<SillyDudeVmo> LoadSillyDude(int id)
        {
            var dude = await _dudeService.GetSilly(id);
            return new SillyDudeVmo(dude, new TapCommand(url => Device.OpenUri(new Uri((string)url))));
        }
    }
}