using System;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.Helpers;
using Sharpnado.Presentation.Forms.RenderedViews;
using Sharpnado.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using Xamarin.Forms.Xaml;
using NavigationPage = Xamarin.Forms.NavigationPage;

namespace SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyBottomTabsPage : SillyContentPage, IBindablePage
    {
        private Theme _currentTheme;

        public SillyBottomTabsPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();

            TabButton.TapCommand = new Command(() => System.Diagnostics.Debug.WriteLine("TapButton tapped!"));

            _currentTheme = Theme.Dark;
            ApplyTheme();

            GridContainer.RaiseChild(Toolbar);
            GridContainer.RaiseChild(TabHost);
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
                return;
            }

            ResourcesHelper.SetDarkMode();
        }

        private void TabButtonOnClicked(object sender, EventArgs e)
        {
            TaskMonitor.Create(AnimateTabButton);
        }

        private async Task AnimateTabButton()
        {
            double sourceScale = TabButton.Scale;
            Color sourceColor = TabButton.ButtonBackgroundColor;
            Color targetColor = _currentTheme == Theme.Light
                ? ResourcesHelper.GetResourceColor("DarkSurface")
                : Color.White;

            await TabButton.ScaleTo(3);
            await TabButton.ScaleTo(sourceScale);
            TabButton.IconImageSource = null;

            var bigScaleTask = TabButton.ScaleTo(30, length: 500);
            var colorChangeTask = TabButton.ColorTo(
                sourceColor,
                targetColor,
                callback: c => TabButton.ButtonBackgroundColor = c,
                length: 500);
            await Task.WhenAll(bigScaleTask, colorChangeTask);

            _currentTheme = _currentTheme == Theme.Light ? Theme.Dark : Theme.Light;
            ApplyTheme();

            var reverseBigScaleTask = TabButton.ScaleTo(sourceScale, length: 500);
            var reverseColorChangeTask = TabButton.ColorTo(
                targetColor,
                sourceColor,
                c => TabButton.ButtonBackgroundColor = c,
                length: 500);

            await Task.WhenAll(reverseBigScaleTask, reverseColorChangeTask);

            TabButton.IconImageSource = "theme_96.png";
        }
    }
}