// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainActivity.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SillyCompany.Mobile.Practices.Droid
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;

    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// </summary>
    [Activity(Theme = "@style/ZoliTheme", ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        /// <summary>
        /// </summary>
        /// <param name="bundle">
        /// </param>
        protected override void OnCreate(Bundle bundle)
        {
            ToolbarResource = Resource.Layout.toolbar;

            base.OnCreate(bundle);
            
            Xamarin.Forms.Forms.Init(this, bundle);

            this.LoadApplication(new App());
        }
    }
}