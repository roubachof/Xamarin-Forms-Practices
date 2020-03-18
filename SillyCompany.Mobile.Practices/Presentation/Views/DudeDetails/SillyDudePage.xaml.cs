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

namespace SillyCompany.Mobile.Practices.Presentation.Views.DudeDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyDudePage : SillyContentPage, IBindablePage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SillyDudePage"/> class.
        /// </summary>
        public SillyDudePage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();

            GridContainer.RaiseChild(TabHost);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private void ScrollViewOnScrolled(object sender, ScrolledEventArgs e)
        {
            if (Image.Height < 1)
            {
                return;
            }

            double alpha = e.ScrollY / Image.Height;

            var accentColor = (Color)Application.Current.Resources["Accent"];

            Toolbar.BackgroundColor = accentColor.MultiplyAlpha(alpha);
        }
    }
}