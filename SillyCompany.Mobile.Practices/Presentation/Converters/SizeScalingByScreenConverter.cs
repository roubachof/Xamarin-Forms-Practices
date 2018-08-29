using System;
using System.Globalization;
using SillyCompany.Mobile.Practices.Infrastructure;
using Xamarin.Forms;

namespace SillyCompany.Mobile.Practices.Presentation.Converters
{
    public class SizeScalingByScreenConverter : IValueConverter
    {
        public static SizeScalingByScreenConverter Instance { get; } = new SizeScalingByScreenConverter();

        public double Convert(double value)
        {
            return (double)Convert(value, null, null, null);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch (PlatformService.ScreenSize)
            {
                case ScreenSize.Small:
                    return (double)value;
                case ScreenSize.Regular:
                    return (double)value * 1.33;
                default:
                    return (double)value * 1.5;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
