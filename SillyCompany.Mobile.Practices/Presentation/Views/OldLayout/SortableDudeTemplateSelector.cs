using SillyCompany.Mobile.Practices.Presentation.ViewModels;

using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.Views
{
    public class SortableDudeTemplateSelector : DataTemplateSelector
    {
        public DataTemplate AddSillyDude { get; set; }

        public DataTemplate SillyDude { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            if (item is AddSillyDudeVmo)
            {
                return AddSillyDude;
            }

            return SillyDude;
        }
    }
}