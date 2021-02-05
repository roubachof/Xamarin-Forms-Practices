// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;

using Microsoft.Device.Display;

using Sharpnado.HorizontalListView.Droid;
using Sharpnado.MaterialFrame.Droid;

using SillyCompany.Mobile.Practices.Infrastructure;

using Xamarin.Duo.Forms.Samples;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SillyCompany.Mobile.Practices.Droid
{
    [Activity(
        Theme = "@style/ZoliTheme",
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
        WindowSoftInputMode = SoftInput.AdjustResize)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);

            var screenHelper = new ScreenHelper();
            bool isDuo = screenHelper.Initialize(this);

            PlatformService.InitializeFoldingScreen(isDuo);

            PlatformService.Initialize(
                IsRunningInEmulator(),
                Resources.DisplayMetrics.Density,
                (int)(Resources.DisplayMetrics.WidthPixels / Resources.DisplayMetrics.Density),
                (int)(Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density),
                () => new Thickness(0, 24, 0, 0));

            HingeService.MainActivity = this;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat && Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                Window.SetFlags(WindowManagerFlags.TranslucentStatus, WindowManagerFlags.TranslucentStatus);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
            {
                Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.LayoutStable | SystemUiFlags.LayoutFullscreen);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.ClearFlags(WindowManagerFlags.TranslucentStatus);
                Window.SetStatusBarColor(Android.Graphics.Color.Transparent);
            }

            if (Build.VERSION.SdkInt >= BuildVersionCodes.LollipopMr1)
            {
                Window.DecorView.SystemUiVisibility =
                    (StatusBarVisibility)((SystemUiFlags)Window.DecorView.SystemUiVisibility
                        | SystemUiFlags.LightStatusBar);
            }

            Forms.Init(this, bundle);

            SharpnadoInitializer.Initialize(enableInternalLogger: true, enableInternalDebugLogger: true);
            Android.Glide.Forms.Init(this);
            AndroidMaterialFrameRenderer.ThrowStopExceptionOnDraw = false;
            AndroidMaterialFrameRenderer.BlurAutoUpdateDelayMilliseconds = 200;
            AndroidMaterialFrameRenderer.BlurProcessingDelayMilliseconds = 100;

            this.LoadApplication(new App());
        }

        private static bool IsRunningInEmulator()
        {
            return (Build.Brand.StartsWith("generic") && Build.Device.StartsWith("generic"))
                || Build.Fingerprint.StartsWith("generic")
                || Build.Fingerprint.StartsWith("unknown")
                || Build.Hardware.Contains("goldfish")
                || Build.Hardware.Contains("ranchu")
                || Build.Model.Contains("google_sdk")
                || Build.Model.Contains("Emulator")
                || Build.Model.Contains("Android SDK built for x86")
                || Build.Manufacturer.Contains("Genymotion")
                || Build.Product.Contains("sdk_google")
                || Build.Product.Contains("google_sdk")
                || Build.Product.Contains("sdk")
                || Build.Product.Contains("sdk_x86")
                || Build.Product.Contains("vbox86p")
                || Build.Product.Contains("emulator")
                || Build.Product.Contains("simulator");
        }
    }
}