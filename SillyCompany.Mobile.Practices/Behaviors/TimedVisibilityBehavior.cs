namespace SillyCompany.Mobile.Practices.Behaviors
{
    using System;
    using System.Threading.Tasks;

    using Xamarin.Forms;

    public class TimedVisibilityBehavior : Behavior<View>
    {
        private bool lastVisibility;

        protected override void OnAttachedTo(View bindable)
        {
            this.lastVisibility = bindable.IsVisible;
            bindable.PropertyChanged += this.ViewPropertyChanged;
        }

        private async void ViewPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var view = (View)sender;
            if (e.PropertyName == "IsVisible")
            {
                if (!this.lastVisibility && view.IsVisible)
                {
                    await Task.Delay(TimeSpan.FromSeconds(5));
                    Device.BeginInvokeOnMainThread(() => view.IsVisible = false);
                }
                else
                {
                    this.lastVisibility = view.IsVisible;
                }
            }
        }
    }
}