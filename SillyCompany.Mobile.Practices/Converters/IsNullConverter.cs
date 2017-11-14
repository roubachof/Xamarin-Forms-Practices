namespace SillyCompany.Mobile.Practices.Converters
{
    using System;
    using System.Globalization;

    using Xamarin.Forms;

    public class IsNullConverter : IValueConverter
    {
        /// <summary>
        /// Returns true is value == null.
        /// </summary>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
