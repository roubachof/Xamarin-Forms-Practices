// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SillyDudePage.xaml.cs" company="The Silly Company">
//   The Silly Company 2016. All rights reserved.
// </copyright>
// <summary>
//   The silly dude page.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyDudePage : ContentPage, IBindablePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SillyDudePage"/> class.
        /// </summary>
        public SillyDudePage()
        {
            InitializeComponent();
        }

        private NavigationPage NavigationPage => (NavigationPage)Application.Current.MainPage;

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
    }
}