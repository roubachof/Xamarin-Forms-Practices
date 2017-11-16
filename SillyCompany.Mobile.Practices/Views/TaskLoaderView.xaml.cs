using SillyCompany.Mobile.Practices.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BindableObject = Xamarin.Forms.BindableObject;

namespace SillyCompany.Mobile.Practices.Views
{
    [ContentProperty("Child")]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TaskLoaderView : ContentView
    {
        public static readonly BindableProperty ViewModelLoaderProperty = BindableProperty.Create(
            nameof(ViewModelLoader),
            typeof(IViewModelLoader),
            typeof(TaskLoaderView), 
            propertyChanged: ViewModelLoaderChanged);

        private static void ViewModelLoaderChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var taskLoader = (TaskLoaderView)bindable;
            taskLoader.SetBindings();
        }

        protected ContentView ContentContainer;

        public IViewModelLoader ViewModelLoader
        {
            get => (IViewModelLoader)GetValue(ViewModelLoaderProperty);
            set => SetValue(ViewModelLoaderProperty, value);
        }

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

        private void SetBindings()
        {
            if (ViewModelLoader != null)
            {
                ContentContainer.SetBinding(
                    ContentView.IsVisibleProperty,
                    new Binding(nameof(ViewModelLoader.ShowResult), source: ViewModelLoader));

                Loader.SetBinding(
                    ActivityIndicator.IsRunningProperty,
                    new Binding(nameof(ViewModelLoader.ShowLoader), source: ViewModelLoader));

                ErrorView.SetBinding(
                    StackLayout.IsVisibleProperty,
                    new Binding(nameof(ViewModelLoader.ShowError), source: ViewModelLoader));

                ErrorViewLabel.SetBinding(
                    Label.TextProperty,
                    new Binding(nameof(ViewModelLoader.ErrorMessage), source: ViewModelLoader));

                ErrorViewButton.SetBinding(
                    Button.CommandProperty,
                    new Binding(nameof(ViewModelLoader.ReloadCommand), source: ViewModelLoader));

                ErrorNotificationView.SetBinding(
                    Frame.IsVisibleProperty,
                    new Binding(nameof(ViewModelLoader.ShowErrorNotification), source: ViewModelLoader, mode: BindingMode.TwoWay));

                ErrorNotificationViewLabel.SetBinding(
                    Label.TextProperty,
                    new Binding(nameof(ViewModelLoader.ErrorMessage), source: ViewModelLoader));
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

            Grid.Children.Insert(0, ContentContainer);
        }
    }
}