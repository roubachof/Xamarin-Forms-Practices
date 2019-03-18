using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            defaultValue: false);

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

        private const int ShadowHeight = 6;

        public Toolbar()
        {
            InitializeComponent();

            var navigationService = DependencyContainer.Instance.GetInstance<INavigationService>();

            TapCommandEffect.SetTap(BackButton, AsyncCommand.Create(() => navigationService.NavigateBackAsync()));

            UpdateShadow();
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

        private static void HasShadowPropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            var toolbar = (Toolbar)bindable;
            toolbar.UpdateShadow();
        }

        private void UpdateShadow()
        {
            if (HasShadow)
            {
                ShadowRowDefinition.Height = new GridLength(ShadowHeight);
                ShadowBoxView.IsVisible = true;
                Margin = new Thickness(Margin.Left, Margin.Top, Margin.Right, Margin.Bottom - ShadowHeight);

                var boxView = new BoxView { BackgroundColor = BackgroundColor };
                BackgroundColor = Color.Transparent;
                Grid.Children.Insert(0, boxView);
                Grid.SetRow(boxView, 0);
                Grid.SetColumnSpan(boxView, 3);
            }
            else
            {
                ShadowRowDefinition.Height = new GridLength(0);
                ShadowBoxView.IsVisible = false;
            }
        }
    }
}