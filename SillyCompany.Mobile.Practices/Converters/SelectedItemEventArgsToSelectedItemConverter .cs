//
// From https://raw.githubusercontent.com/davidbritch/xamarin-forms/master/ItemSelectedBehavior/ItemSelectedBehavior/Converters/SelectedItemEventArgsToSelectedItemConverter.cs
//
namespace SillyCompany.Mobile.Practices.Converters
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public class SelectedItemEventArgsToSelectedItemConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var eventArgs = value as SelectedItemChangedEventArgs;
            return eventArgs.SelectedItem;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
