using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Sharpnado.Presentation.Forms.RenderedViews;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeView : ContentView
    {
        public HomeView()
        {
            InitializeComponent();
        }

        public void LogMaterialFrameContent()
        {
            var dumpedFrame = PlatformHelper.Instance.DumpNativeViewHierarchy(MaterialFrame, true);

            System.Diagnostics.Debug.WriteLine(dumpedFrame);
        }
    }
}