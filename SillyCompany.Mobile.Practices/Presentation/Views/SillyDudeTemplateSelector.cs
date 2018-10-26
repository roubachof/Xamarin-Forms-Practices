using SillyCompany.Mobile.Practices.Presentation.ViewModels;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    public class SillyDudeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate SillySquare { get; set; }

        public DataTemplate AlternateSillySquare { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is SillyDudeVmo sillyDudeVmo)
            {
                if (sillyDudeVmo.SillinessDegree >= 4)
                {
                    return AlternateSillySquare;
                }

                return SillySquare;
            }

            return null;
        }
    }
}
