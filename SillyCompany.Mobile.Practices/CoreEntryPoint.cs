// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreEntryPoint.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The core entry point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using MetroLog;
using MetroLog.Targets;

namespace SillyCompany.Mobile.Practices
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using SillyCompany.Mobile.Practices.Services;
    using SillyCompany.Mobile.Practices.Services.Navigables;
    using SillyCompany.Mobile.Practices.Services.Navigables.Impl;

    using Xamarin.Forms;

    /// <summary>
    /// The core entry point.
    /// </summary>
    public class CoreEntryPoint
    {
        /// <summary>
        /// The project assembly.
        /// </summary>
        private static readonly Assembly ProjectAssembly = typeof(CoreEntryPoint).GetTypeInfo().Assembly;

        /// <summary>
        /// The register dependencies async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task RegisterDependenciesAsync()
        {
            await Task.Run(() => this.RegisterDependencies());
        }

        /// <summary>
        /// The register dependencies.
        /// </summary>
        private void RegisterDependencies()
        {
            var container = DependencyContainer.Instance;

            container.RegisterSingleton(
                () => new Lazy<NavigationPage>(() => (NavigationPage)Application.Current.MainPage));

            container.RegisterSingleton<IViewLocator, ViewLocator>();
            container.RegisterSingleton<ISillyFrontService, SillyFrontService>();
            container.RegisterSingleton<INavigationService, FormsNavigationService>();

            // Register all views by convention
            foreach (var pageType in 
                ProjectAssembly.ExportedTypes.Where(
                    type =>
                    type.Namespace.StartsWith("SillyCompany.Mobile.Practices.Views") && !type.GetTypeInfo().IsAbstract
                    && type.Name.EndsWith("Page")))
            {
                container.Register(pageType);
            }

            // Register all view models by convention
            foreach (var viewModelType in 
                ProjectAssembly.ExportedTypes.Where(
                    type =>
                    type.Namespace.StartsWith("SillyCompany.Mobile.Practices.ViewModels")
                    && !type.GetTypeInfo().IsAbstract && type.Name.EndsWith("Vm")))
            {
                container.Register(viewModelType);
            }

            InitializeLogTargets();
        }

        private void InitializeLogTargets()
        {
            var config = new LoggingConfiguration();

            config.AddTarget(LogLevel.Info, LogLevel.Fatal, new DebugTarget());

            LoggerFactory.Initialize(config);
        }
    }
}