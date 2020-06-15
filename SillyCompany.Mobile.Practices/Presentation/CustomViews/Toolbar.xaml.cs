using System.Runtime.CompilerServices;

using Sharpnado.Presentation.Forms.Effects;

using SillyCompany.Mobile.Practices.Presentation.Commands;
using SillyCompany.Mobile.Practices.Presentation.Navigables;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Toolbar : ContentView
    {
        public static readonly BindableProperty ShowBackButtonProperty = BindableProperty.Create(
            nameof(ShowBackButton),
            typeof(bool),
            typeof(Toolbar),
            defaultValue: false,
            propertyChanged: ShowBackButtonPropertyChanged);

        public static readonly BindableProperty ForegroundColorProperty = BindableProperty.Create(
            nameof(ForegroundColor),
            typeof(Color),
            typeof(Toolbar));

        public static readonly BindableProperty TitleProperty = BindableProperty.Create(
            nameof(Title),
            typeof(string),
            typeof(Toolbar),
            string.Empty);

        public static readonly BindableProperty SubtitleProperty = BindableProperty.Create(
            nameof(Subtitle),
            typeof(string),
            typeof(Toolbar),
            string.Empty,
            propertyChanged: SubtitlePropertyChanged);

        public Toolbar()
        {
            InitializeComponent();

            var navigationService = DependencyContainer.Instance.GetInstance<INavigationService>();

            TapCommandEffect.SetTap(BackButton, AsyncCommand.Create(() => navigationService.NavigateBackAsync()));
        }

        public bool ShowBackButton
        {
            get => (bool)GetValue(ShowBackButtonProperty);
            set => SetValue(ShowBackButtonProperty, value);
        }

        public Color ForegroundColor
        {
            get => (Color)GetValue(ForegroundColorProperty);
            set => SetValue(ForegroundColorProperty, value);
        }

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Subtitle
        {
            get => (string)GetValue(SubtitleProperty);
            set => SetValue(SubtitleProperty, value);
        }

        protected override void OnPropertyChanging([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanging(propertyName);
        }

        private static void ShowBackButtonPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var toolbar = (Toolbar)bindable;
            toolbar.UpdateShowBackButton();
        }

        private static void SubtitlePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var toolbar = (Toolbar)bindable;
            toolbar.UpdateSubtitle();
        }

        private void UpdateShowBackButton()
        {
            ButtonColumnDefinition.Width = ShowBackButton ? 50 : 0;
        }

        private void UpdateSubtitle()
        {
            SubtitleRowDefinition.Height = string.IsNullOrEmpty(Subtitle) ? 0 : GridLength.Auto;
        }
    }
}