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
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            var screenHelper = new ScreenHelper();
            bool isDuo = screenHelper.Initialize(this);

            PlatformService.InitializeFoldingScreen(isDuo);

            HingeService.MainActivity = this;

            Forms.Init(this, bundle);
            Android.Glide.Forms.Init(this);

            this.LoadApplication(new App());
        }
    }
}