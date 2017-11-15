using System;

using Foundation;
using ImageCircle.Forms.Plugin.iOS;
using Refractored.XamForms.PullToRefresh.iOS;
using UIKit;

namespace SillyCompany.Mobile.Practices.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            Initialize();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void Initialize()
        {
            try
            {
                new CoreEntryPoint().RegisterDependencies();
                ImageCircleRenderer.Init();
                PullToRefreshLayoutRenderer.Init();
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception while initializing app: {exception.Message}");
                throw;
            }
        }
    }
}
