// --------------------------------------------------------------------------------------------------------------------
// <copyright file="App.xaml.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The app.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using MetroLog;

using Sharpnado.Presentation.Forms.RenderedViews;

using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.ViewModels;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.SurfaceDuo;
using SillyCompany.Mobile.Practices.Presentation.ViewModels.TabsLayout;
using SillyCompany.Mobile.Practices.Presentation.Views;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices
{
    /// <summary>
    /// The app.
    /// </summary>
    public partial class App : Application
    {
        private static readonly ILogger Logger = LoggerFactory.GetLogger(nameof(App));

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            InitializeComponent();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainUnhandledException;

            Sharpnado.Tabs.Initializer.Initialize(true, true);

            var viewLocator = DependencyContainer.Instance.GetInstance<IViewLocator>();

            IBindablePage firstScreenView;
            if (PlatformService.IsFoldingScreen)
            {
                firstScreenView = viewLocator.GetViewFor<TwoPanePageViewModel>();
            }
            else
            {
#if OLD
                firstScreenView = viewLocator.GetViewFor<SillyPeopleVm>();
#else
                firstScreenView = viewLocator.GetViewFor<SillyBottomTabsPageViewModel>();
#endif
            }

            MainPage = new NavigationPage((Page)firstScreenView);
            NavigationPage.SetHasNavigationBar(MainPage, false);
            var firstScreenVm = (ANavigableViewModel)firstScreenView.BindingContext;
            firstScreenVm.Load(null);
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger.Error($"Unhandled exception: {e}");
        }

        /// <summary>
        /// The on parent set.
        /// </summary>
        protected override void OnParentSet()
        {
        }

        /// <summary>
        /// The on start.
        /// </summary>
        protected override void OnStart()
        {
            // Handle when your app starts
            // Initiate Navigation and navigate to the splashscreen
        }

        /// <summary>
        /// The on sleep.
        /// </summary>
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        /// <summary>
        /// The on resume.
        /// </summary>
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}