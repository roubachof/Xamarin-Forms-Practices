using SillyCompany.Mobile.Practices.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(iOSNavigationBarRenderer))]
namespace SillyCompany.Mobile.Practices.iOS.Renderers
{
    public class iOSNavigationBarRenderer : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (Element == null)
            {
                return;
            }

            // iOSMaterialFrameRenderer.AddShadow(NavigationBar.Layer, 4);
        }
    }
}