using System;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.RenderedViews;
using Sharpnado.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.SurfaceDuo
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TwoPanePage : ContentPage, IBindablePage
    {
        private Theme _currentTheme;

        public TwoPanePage()
        {
            SetValue(Xamarin.Forms.NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();

            TabButton.TapCommand = new Command(() => System.Diagnostics.Debug.WriteLine("TapButton tapped!"));

            _currentTheme = Theme.Dark;
            ApplyTheme();
        }

        private enum Theme
        {
            Light = 0,
            Dark,
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var safeArea = On<iOS>().SafeAreaInsets();
            BottomSafeAreaDefinition.Height = safeArea.Bottom;
            Padding = 0;
        }

        private void ApplyTheme()
        {
            if (_currentTheme == Theme.Light)
            {
                ResourcesHelper.SetLightMode();
                TabHost.ShadowType = ShadowType.None;
                return;
            }

            ResourcesHelper.SetDarkMode();
            TabHost.ShadowType = ShadowType.None;
        }

        private void TabButtonOnClicked(object sender, EventArgs e)
        {
            TaskMonitor.Create(AnimateTabButton);

            _currentTheme = _currentTheme == Theme.Light ? Theme.Dark : Theme.Light;
            ApplyTheme();
        }

        private async Task AnimateTabButton()
        {
            await TabButton.ScaleTo(2);
            await TabButton.ScaleTo(1);
            await TabButton.ScaleTo(2);
            await TabButton.ScaleTo(1);
        }
    }
}