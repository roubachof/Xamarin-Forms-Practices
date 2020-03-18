using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SillyCompany.Mobile.Practices.Infrastructure;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.Views.TabsLayout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListView : ContentView
    {
        public ListView()
        {
            InitializeComponent();

            Picker.ItemsSource = ErrorEmulator.ErrorLabels;
        }
    }
}