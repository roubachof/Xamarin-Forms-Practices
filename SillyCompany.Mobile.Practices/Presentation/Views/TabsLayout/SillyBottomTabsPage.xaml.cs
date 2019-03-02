using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyBottomTabsPage : ContentPage, IBindablePage
    {
        public SillyBottomTabsPage()
        {
            InitializeComponent();
        }
    }
}