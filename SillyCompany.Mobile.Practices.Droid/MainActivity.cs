// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
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

            Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new App());
        }
    }
}