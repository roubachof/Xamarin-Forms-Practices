using System;
using System.Text;
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
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();


            var fontList = new StringBuilder();
            var familyNames = UIFont.FamilyNames;
            foreach (var familyName in familyNames)
            {
                fontList.Append(String.Format("Family: {0}\n", familyName));
                Console.WriteLine("Family: {0}\n", familyName);
                var fontNames = UIFont.FontNamesForFamilyName(familyName);
                foreach (var fontName in fontNames)
                {
                    Console.WriteLine("\tFont: {0}\n", fontName);
                    fontList.Append(String.Format("\tFont: {0}\n", fontName));
                }
            };

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

                //UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
                //{
                //    TextColor = UIColor.White,
                //    Font = UIFont.FromName("ComicSansMS", 24),
                //});
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception while initializing app: {exception.Message}");
                throw;
            }
        }
    }
}
