using System;
using System.Text;
using Foundation;
using ObjCRuntime;

using Sharpnado.CollectionView.iOS;
using Sharpnado.MaterialFrame.iOS;
using SillyCompany.Mobile.Practices.Infrastructure;
using UIKit;

using Xamarin.Forms;

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

            Initialize();
            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        private void Initialize()
        {
            try
            {
                PlatformService.Initialize(
                    Runtime.Arch == Arch.SIMULATOR,
                    UIScreen.MainScreen.Scale,
                    (int)UIScreen.MainScreen.Bounds.Width,
                    (int)UIScreen.MainScreen.Bounds.Height,
                    SafeAreaGetter);

                new CoreEntryPoint().RegisterDependencies();

                Initializer.Initialize(enableInternalLogger: true);
                iOSMaterialFrameRenderer.Init();
                Sharpnado.Tabs.iOS.Preserver.Preserve();

                Xamarin.Forms.Nuke.FormsHandler.Init(debug: true);

                UINavigationBar.Appearance.SetTitleTextAttributes(new UITextAttributes
                {
                    TextColor = UIColor.White,
                    Font = UIFont.FromName("OpenSans-SemiBold", 17),
                });
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Exception while initializing app: {exception.Message}");
                throw;
            }
        }

        private static Thickness SafeAreaGetter()
        {
            UIEdgeInsets safeArea;

            if (!UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                safeArea = new UIEdgeInsets(UIApplication.SharedApplication.StatusBarFrame.Size.Height, 0, 0, 0);
            }
            else if (UIApplication.SharedApplication.KeyWindow != null)
            {
                safeArea = UIApplication.SharedApplication.KeyWindow.SafeAreaInsets;
            }
            else if (UIApplication.SharedApplication.Windows.Length > 0)
            {
                safeArea = UIApplication.SharedApplication.Windows[0].SafeAreaInsets;
            }
            else
            {
                safeArea = UIEdgeInsets.Zero;
            }

            return new Thickness(
                safeArea.Left,
                safeArea.Top > 10 ? safeArea.Top - 10 : safeArea.Top,
                safeArea.Right,
                safeArea.Bottom > 10 ? safeArea.Bottom - 10 : safeArea.Bottom);
        }
    }
}
