using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Views
{
    [ContentProperty("Child")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskLoaderView : ContentView
    {   
        protected ContentView ContentContainer;

        public View Child
        {
            get => ContentContainer.Content;
            set
            {
                if (Grid.Children.Contains(value))
                    return;

                ContentContainer.Content = value;
            }
        }

        public TaskLoaderView()
        {
            InitializeComponent();

            ContentContainer = new ContentView
            {
                VerticalOptions = LayoutOptions.Fill,
                HorizontalOptions = LayoutOptions.Fill,
            };

            ContentContainer.SetBinding(ContentView.IsVisibleProperty, "ShowResult");
            
            Grid.Children.Insert(0, ContentContainer);
        }
    }
}