using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.RenderedViews;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        private int _currentRotation = 0;

        public HomeView()
        {
            InitializeComponent();
        }

        public void LogMaterialFrameContent()
        {
            var dumpedFrame = PlatformHelper.Instance.DumpNativeViewHierarchy(MaterialFrame, true);

            System.Diagnostics.Debug.WriteLine(dumpedFrame);

            // TaskMonitor.Create(Animate);
        }

        private async Task Animate()
        {
            await MaterialFrame.ScaleTo(0.5);
            await MaterialFrame.ScaleTo(1);

            _currentRotation = (_currentRotation + 90) % 360;
            await MaterialFrame.RotateTo(_currentRotation);
        }
    }
}