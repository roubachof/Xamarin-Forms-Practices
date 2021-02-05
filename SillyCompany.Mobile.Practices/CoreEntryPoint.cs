// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CoreEntryPoint.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The core entry point.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using MetroLog;
using MetroLog.Targets;
using SillyCompany.Mobile.Practices.Domain.Silly;
using SillyCompany.Mobile.Practices.Infrastructure;
using SillyCompany.Mobile.Practices.Presentation.Navigables;
using SillyCompany.Mobile.Practices.Presentation.Navigables.Impl;

using SimpleInjector;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices
{
    /// <summary>
    /// The core entry point.
    /// </summary>
    public class CoreEntryPoint
    {
        private static readonly Assembly ProjectAssembly = typeof(CoreEntryPoint).GetTypeInfo().Assembly;

        public async Task RegisterDependenciesAsync()
        {
            await Task.Run(() => RegisterDependencies());
        }

        public void RegisterDependencies()
        {
            var container = DependencyContainer.Instance;

            container.Options.EnableAutoVerification = false;
            container.Options.ResolveUnregisteredConcreteTypes = true;

            container.RegisterSingleton(
                () => new Lazy<NavigationPage>(() => (NavigationPage)Application.Current.MainPage));

            container.RegisterSingleton<IViewLocator, ViewLocator>();
            container.RegisterSingleton<ISillyDudeService, SillyDudeService>();
            container.RegisterSingleton<INavigationService, FormsNavigationService>();
            container.RegisterSingleton<ErrorEmulator>();

            // Register all views by convention
            foreach (var pageType in
                ProjectAssembly.ExportedTypes.Where(
                    type =>
                    type.Namespace.StartsWith("SillyCompany.Mobile.Practices.Presentation.Views") && !type.GetTypeInfo().IsAbstract
                    && type.Name.EndsWith("Page")))
            {
                container.Register(pageType);
            }

            // Register all view models by convention
            foreach (var viewModelType in
                ProjectAssembly.ExportedTypes.Where(
                    type =>
                    type.Namespace.StartsWith("SillyCompany.Mobile.Practices.Presentation.ViewModels")
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