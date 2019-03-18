using System.Windows.Input;

namespace SillyCompany.Mobile.Practices.Presentation.ViewModels.DudeDetails
{
    public class QuoteVmo
    {
        public QuoteVmo(string sourceUrl, string quote, ICommand onItemTappedCommand)
        {
            SourceUrl = sourceUrl;
            Quote = quote;
            OnItemTappedCommand = onItemTappedCommand;
        }

        public ICommand OnItemTappedCommand { get; set; }

        public string SourceUrl { get; }

        public string Quote { get; }
    }
}