using Sharpnado.Presentation.Forms.RenderedViews;

using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SillySquareCell : MaterialFrame
    {
        public SillySquareCell()
        {
            InitializeComponent();
        }
    }
}