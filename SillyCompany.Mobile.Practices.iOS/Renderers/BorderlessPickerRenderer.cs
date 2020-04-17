using System.ComponentModel;

using Foundation;

using SillyCompany.Mobile.Practices.iOS.Renderers;

using UIKit;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Picker), typeof(BorderlessPickerRenderer))]

namespace SillyCompany.Mobile.Practices.iOS.Renderers
{
    [Preserve]
    public class BorderlessPickerRenderer : PickerRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
        }
    }
}