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
        public static readonly new BindableProperty BackgroundColorProperty = BindableProperty.Create(
            nameof(BackgroundColor),
            typeof(Color),
            typeof(Toolbar),
            Color.Transparent);

        public static readonly BindableProperty ShowBackButtonProperty = BindableProperty.Create(
            nameof(ShowBackButton),
            typeof(bool),
            typeof(Toolbar),
            defaultValue: false,
            propertyChanged: ShowBackButtonPropertyChanged);

        public static readonly BindableProperty HasShadowProperty = BindableProperty.Create(
            nameof(HasShadow),
            typeof(bool),
            typeof(Toolbar),
            defaultValue: false,
            propertyChanged: HasShadowPropertyChanged);

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

        private const int ShadowHeight = 6;

        private bool _shadowAdded;
        private BoxView _backgroundBoxView;

        private bool _innerSetterBackfire;

        public Toolbar()
        {
            InitializeComponent();

            var navigationService = DependencyContainer.Instance.GetInstance<INavigationService>();

            TapCommandEffect.SetTap(BackButton, AsyncCommand.Create(() => navigationService.NavigateBackAsync()));

            UpdateShadow();
        }

        public new Color BackgroundColor
        {
            get => (Color)GetValue(BackgroundColorProperty);
            set => SetValue(BackgroundColorProperty, value);
        }

        public bool HasShadow
        {
            get => (bool)GetValue(HasShadowProperty);
            set => SetValue(HasShadowProperty, value);
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

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(BackgroundColor) && !_innerSetterBackfire)
            {
                if (_shadowAdded)
                {
                    _backgroundBoxView.Color = BackgroundColor;
                }
                else
                {
                    base.BackgroundColor = BackgroundColor;
                }
            }
        }

        private static void ShowBackButtonPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var toolbar = (Toolbar)bindable;
            toolbar.UpdateShowBackButton();
        }

        private static void HasShadowPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var toolbar = (Toolbar)bindable;
            toolbar.UpdateShadow();
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

        private void UpdateShadow()
        {
            if (HasShadow)
            {
                Margin = new Thickness(Margin.Left, Margin.Top, Margin.Right, Margin.Bottom - ShadowHeight);
                ShadowBoxView.IsVisible = true;
                ShadowRowDefinition.Height = new GridLength(ShadowHeight);

                if (!_shadowAdded)
                {
                    _backgroundBoxView = new BoxView
                    {
                        Color = BackgroundColor,
                    };

                    _innerSetterBackfire = true;
                    base.BackgroundColor = Color.Transparent;
                    _innerSetterBackfire = false;

                    Grid.Children.Insert(0, _backgroundBoxView);
                    Grid.SetRow(_backgroundBoxView, 0);
                    Grid.SetRowSpan(_backgroundBoxView, 3);
                    Grid.SetColumnSpan(_backgroundBoxView, 3);

                    _shadowAdded = true;
                }
            }
            else
            {
                if (_shadowAdded)
                {
                    Margin = new Thickness(Margin.Left, Margin.Top, Margin.Right, Margin.Bottom + ShadowHeight);
                }

                ShadowRowDefinition.Height = new GridLength(0);
                ShadowBoxView.IsVisible = false;
            }
        }

        private void UpdateSubtitle()
        {
            SubtitleRowDefinition.Height = string.IsNullOrEmpty(Subtitle) ? 0 : GridLength.Auto;
        }
    }
}