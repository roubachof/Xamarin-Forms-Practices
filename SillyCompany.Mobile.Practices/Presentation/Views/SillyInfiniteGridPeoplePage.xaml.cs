using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillyInfiniteGridPeoplePage : ContentPage, IBindablePage
    {
        public SillyInfiniteGridPeoplePage()
        {
            InitializeComponent();
        }
    }
}