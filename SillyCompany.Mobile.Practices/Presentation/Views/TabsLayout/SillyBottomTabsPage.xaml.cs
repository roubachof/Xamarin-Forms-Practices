using System;
using System.Threading.Tasks;

using Sharpnado.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyBottomTabsPage : SillyContentPage, IBindablePage
    {
        public SillyBottomTabsPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
            TabButton.TapCommand = new Command(() => System.Diagnostics.Debug.WriteLine("TapButton tapped!"));
        }

        private void TabButtonOnClicked(object sender, EventArgs e)
        {
            TaskMonitor.Create(AnimateTabButton);
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