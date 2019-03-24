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
        }
    }
}