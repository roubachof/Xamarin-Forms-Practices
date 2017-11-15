// 
// From: https://raw.githubusercontent.com/davidbritch/xamarin-forms/master/ItemSelectedBehavior/ItemSelectedBehavior/Behaviors/ListViewSelectedItemBehavior.cs
// 

using SillyCompany.Mobile.Practices.Commands;

namespace SillyCompany.Mobile.Practices.Behaviors
{
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class SelectedItemBehavior : Behavior<ListView>
    {
        public static readonly BindableProperty CommandProperty = BindableProperty.Create("Command", typeof(IAsyncCommand), typeof(SelectedItemBehavior), null);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create("Converter", typeof(IValueConverter), typeof(SelectedItemBehavior), null);

        public IAsyncCommand Command
        {
            get { return (IAsyncCommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public IValueConverter Converter
        {
            get { return (IValueConverter)this.GetValue(InputConverterProperty); }
            set { this.SetValue(InputConverterProperty, value); }
        }

        public ListView AssociatedObject { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);
            this.AssociatedObject = bindable;
            bindable.BindingContextChanged += this.OnBindingContextChanged;
            bindable.ItemSelected += this.OnListViewItemSelected;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= this.OnBindingContextChanged;
            bindable.ItemSelected -= this.OnListViewItemSelected;
            this.AssociatedObject = null;
        }

        void OnBindingContextChanged(object sender, EventArgs e)
        {
            this.OnBindingContextChanged();
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
            {
                return;
            }
            
            ((ListView)sender).SelectedItem = null;

            if (this.Command == null)
            {
                return;
            }

            object parameter = this.Converter.Convert(e, typeof(object), null, null);
            if (this.Command.CanExecute(parameter))
            {
                await this.Command.ExecuteAsync(parameter);
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.BindingContext = this.AssociatedObject.BindingContext;
        }
    }
}