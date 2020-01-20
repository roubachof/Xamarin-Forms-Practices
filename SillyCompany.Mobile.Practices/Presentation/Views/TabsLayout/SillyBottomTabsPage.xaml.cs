using System;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.RenderedViews;
using Sharpnado.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
        }

        private enum Theme
        {
            Light = 0,
            Dark,
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