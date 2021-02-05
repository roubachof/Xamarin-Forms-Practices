using System;
using System.Threading.Tasks;

using Sharpnado.HorizontalListView.Helpers;
using Sharpnado.Tasks;

using SillyCompany.Mobile.Practices.Infrastructure;

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
        Acrylic = 1,
        Dark = 2,
        Neumorphism = 3,
        AcrylicDarkBlur = 4,
        AcrylicBlur = 5,
    }

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyBottomTabsPage : SillyContentPage, IBindablePage
    {
        private const double LandscapeToolbarHeight = 80;

        private AppTheme _currentAppTheme;

        private double _width;

        private double _height;

        private bool _isLandscape;

        private double _toolbarHeight;
        private GridLength _topAreaHeight;
        private Thickness _tabHostMargin;

        public SillyBottomTabsPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();

            TabButton.TapCommand = new Command(() => System.Diagnostics.Debug.WriteLine("TapButton tapped!"));

            _currentAppTheme = AppTheme.Neumorphism;
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

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height); //must be called
            if (_width != width || _height != height)
            {
                _width = width;
                _height = height;
                bool isLandscape = _width > _height;
                if (!_isLandscape && isLandscape)
                {
                    Grid.SetRow(TabHostLogo, 2);
                    Grid.SetRowSpan(TabHostLogo, 3);

                    double widthRequest = TabHostLogo.HeightRequest;
                    double heightRequest = TabHostLogo.WidthRequest;

                    LayoutOptions horizontal = TabHostLogo.VerticalOptions;
                    LayoutOptions vertical = TabHostLogo.HorizontalOptions;

                    Switcher.Margin = new Thickness(100 + PlatformService.GetSafeArea().Left, 0, 0, 0);

                    _toolbarHeight = Toolbar.Height;
                    _tabHostMargin = TabHostLogo.Margin;
                    _topAreaHeight = TopSafeAreaDefinition.Height;

                    Toolbar.HeightRequest = LandscapeToolbarHeight;
                    Toolbar.UpdateOrientation(true);

                    TabHostLogo.BatchBegin();
                    TabHostLogo.Margin = new Thickness(PlatformService.GetSafeArea().Left, 0, 0, 0);
                    TabHostLogo.Orientation = Sharpnado.Tabs.OrientationType.Vertical;
                    TabHostLogo.WidthRequest = widthRequest;
                    TabHostLogo.HeightRequest = heightRequest;
                    TabHostLogo.HorizontalOptions = horizontal;
                    TabHostLogo.VerticalOptions = vertical;
                    TabHostLogo.BatchCommit();
                }
                else if (_isLandscape && !isLandscape)
                {
                    Grid.SetRow(TabHostLogo, 3);
                    Grid.SetRowSpan(TabHostLogo, 1);

                    double widthRequest = TabHostLogo.HeightRequest;
                    double heightRequest = TabHostLogo.WidthRequest;

                    LayoutOptions horizontal = TabHostLogo.VerticalOptions;
                    LayoutOptions vertical = TabHostLogo.HorizontalOptions;

                    Switcher.Margin = new Thickness(0);
                    // Toolbar.HeightRequest = _toolbarHeight;

                    Toolbar.HeightRequest = -1;

                    Toolbar.UpdateOrientation(false);

                    TabHostLogo.BatchBegin();
                    TabHostLogo.Margin = _tabHostMargin;
                    TabHostLogo.Orientation = Sharpnado.Tabs.OrientationType.Horizontal;
                    TabHostLogo.WidthRequest = widthRequest;
                    TabHostLogo.HeightRequest = heightRequest;
                    TabHostLogo.HorizontalOptions = horizontal;
                    TabHostLogo.VerticalOptions = vertical;
                    TabHostLogo.BatchCommit();
                }

                _isLandscape = isLandscape;
            }
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
                case AppTheme.Neumorphism:
                    ResourcesHelper.SetNeumorphicMode();
                    break;
                case AppTheme.Dark:
                    ResourcesHelper.SetDarkMode();
                    break;
            }
        }

        private void ToolbarOnTapped(object sender, EventArgs e)
        {
            if (_currentAppTheme == AppTheme.AcrylicBlur)
            {
                _currentAppTheme = AppTheme.Light;
            }
            else
            {
                _currentAppTheme += 1;
            }

            ApplyTheme();
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