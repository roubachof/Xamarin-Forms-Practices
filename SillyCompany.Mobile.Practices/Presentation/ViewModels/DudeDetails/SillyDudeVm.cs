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

using Sharpnado.Presentation.Forms;
using Sharpnado.Presentation.Forms.Commands;
using Sharpnado.Presentation.Forms.ViewModels;

using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Presentation.Navigables;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails
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

        private int _selectedViewModelIndex = 0;

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

            SillyDudeLoaderNotifier = new TaskLoaderNotifier<SillyDudeVmo>();
        }

        /// <summary>
        /// Gets or sets the silly dude task.
        /// </summary>
        /// <value>The silly dude task.</value>
        public TaskLoaderNotifier<SillyDudeVmo> SillyDudeLoaderNotifier { get; }

        public QuoteVmo Quote { get; private set; }

        public FilmoVmo Filmo { get; private set; }

        public MemeVmo Meme { get; private set; }

        public int SelectedViewModelIndex
        {
            get => _selectedViewModelIndex;
            set => SetAndRaise(ref _selectedViewModelIndex, value);
        }

        /// <summary>
        /// Loads the specified parameter.
        /// </summary>
        /// <param name="parameter">
        /// The parameter.
        /// </param>
        public override void Load(object parameter)
        {
            SillyDudeLoaderNotifier.Load(() => LoadSillyDude((int)parameter));
        }

        private async Task<SillyDudeVmo> LoadSillyDude(int id)
        {
            var dude = await _dudeService.GetSilly(id);

            Quote = new QuoteVmo(
                dude.SourceUrl,
                dude.Description,
                new TapCommand(url => Device.OpenUri(new Uri((string)url))));
            Filmo = new FilmoVmo(dude.FilmoMarkdown);
            Meme = new MemeVmo(dude.MemeUrl);
            RaisePropertyChanged(nameof(Quote));
            RaisePropertyChanged(nameof(Filmo));
            RaisePropertyChanged(nameof(Meme));

            return new SillyDudeVmo(dude, null);
        }
    }
}