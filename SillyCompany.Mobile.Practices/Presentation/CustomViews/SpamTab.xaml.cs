using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Sharpnado.HorizontalListView.Helpers;
using Sharpnado.Tabs;
using Sharpnado.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SillyCompany.Mobile.Practices.Presentation.CustomViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SpamTab : TabItem
    {
        public static readonly BindableProperty SpamImageProperty = BindableProperty.Create(
            nameof(SpamImage),
            typeof(string),
            typeof(SpamTab),
            string.Empty);

        private double _height = 0;

        public SpamTab()
        {
            InitializeComponent();
        }

        public string SpamImage
        {
            get => (string)GetValue(SpamImageProperty);
            set => SetValue(SpamImageProperty, value);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (height > 0 && height != _height)
            {
                _height = height;
                if (!IsSelected)
                {
                    Foot.TranslationY = -_height;
                    Foot.Opacity = 0;
                    Spam.HeightRequest = _height;
                }
                else
                {
                    Spam.HeightRequest = 0;
                }
            }
        }

        protected override void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == nameof(IsSelected))
            {
                Animate(IsSelected);
            }
        }

        protected override void OnBadgeChanged(BadgeView oldBadge)
        {
            throw new System.NotImplementedException();
        }

        private void Animate(bool isSelected)
        {
            double targetFootOpacity = isSelected ? 1 : 0;
            double targetFootTranslationY = isSelected ? 0 : -_height;

            double targetHeightSpam = isSelected ? 0 : _height;

            TaskMonitor.Create(
                async () =>
                {
                    Task fadeFootTask = Foot.FadeTo(targetFootOpacity, 500);
                    Task translateFootTask = Foot.TranslateTo(0, targetFootTranslationY, 250, Easing.CubicOut);
                    Task heightSpamTask = Spam.HeightRequestTo(targetHeightSpam, 250, Easing.CubicOut);

                    await Task.WhenAll(fadeFootTask, translateFootTask, heightSpamTask);

                    Spam.HeightRequest = targetHeightSpam;
                    Foot.TranslationY = targetFootTranslationY;
                    Foot.Opacity = targetFootOpacity;
                });
        }
    }
}