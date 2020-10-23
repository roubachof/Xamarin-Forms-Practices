using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sharpnado.Presentation.Forms.CustomViews;
using Sharpnado.Tabs;

using Xam.Forms.Markdown;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.DudeDetails
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Filmo : ContentView, IAnimatableReveal
    {
        public Filmo()
        {
            InitializeComponent();

            SetMarkdownTheme();
        }

        public bool Animate { get; set; }

        private void SetMarkdownTheme()
        {
            MarkdownView.Theme = new SillyTheme();
        }

        public class SillyTheme : MarkdownTheme
        {
            public SillyTheme()
            {
                Heading4.FontFamily = "OpenSans-SemiBold";
                Heading4.ForegroundColor = (Color)Application.Current.Resources["DynamicPrimaryTextColor"];

                Heading5.FontFamily = Application.Current.Resources["FontRegular"] as string;
                Heading5.ForegroundColor = (Color)Application.Current.Resources["DynamicPrimaryTextColor"];

                Paragraph.FontFamily = Application.Current.Resources["FontSemiBold"] as string;
                Paragraph.ForegroundColor = (Color)Application.Current.Resources["DynamicPrimaryTextColor"];
                Paragraph.FontSize = 14;
            }
        }
    }
}