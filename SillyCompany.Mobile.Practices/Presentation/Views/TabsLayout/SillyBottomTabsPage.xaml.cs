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
    public enum AppTheme
    {
        Light = 0,
        Dark = 1,
        Acrylic = 2,
        AcrylicDarkBlur = 3,
        AcrylicBlur = 4,
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyBottomTabsPage : SillyContentPage, IBindablePage
    {
        private AppTheme _currentAppTheme;

        public SillyBottomTabsPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();

            TabButton.TapCommand = new Command(() => System.Diagnostics.Debug.WriteLine("TapButton tapped!"));

            _currentAppTheme = AppTheme.Light;
            ApplyTheme();

            GridContainer.RaiseChild(Toolbar);
            GridContainer.RaiseChild(TabHost);
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
            switch (_currentAppTheme)
            {
                case AppTheme.Acrylic:
                    ResourcesHelper.SetLightMode(true);
                    break;
                case AppTheme.AcrylicDarkBlur:
                    ResourcesHelper.SetDarkBlur();
                    break;
                case AppTheme.AcrylicBlur:
                    ResourcesHelper.SetLightBlur();
                    break;
                case AppTheme.Light:
                    ResourcesHelper.SetLightMode(false);
                    break;
                case AppTheme.Dark:
                    ResourcesHelper.SetDarkMode();
                    break;
            }
        }

        private void TabButtonOnClicked(object sender, EventArgs e)
        {
            TaskMonitor.Create(AnimateTabButton);

            ((HomeView)HomeLazyView.Content).LogMaterialFrameContent();
        }

        private async Task AnimateTabButton()
        {
            double sourceScale = TabButton.Scale;
            Color sourceColor = TabButton.ButtonBackgroundColor;
            Color targetColor = _currentAppTheme == AppTheme.Light
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

            if (_currentAppTheme == AppTheme.AcrylicBlur)
            {
                _currentAppTheme = AppTheme.Light;
            }
            else
            {
                _currentAppTheme += 1;
            }

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