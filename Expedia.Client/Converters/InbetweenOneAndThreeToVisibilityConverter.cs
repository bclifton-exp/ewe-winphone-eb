using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class IntBetweenOneAndThreeToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, String language)
        {
            if (value is int && targetType == typeof(Visibility))
            {
                return ((int)value) < 1 || ((int)value) > 3 ? Visibility.Collapsed : Visibility.Visible;
            }
            return "{null}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, String language)
        {
            throw new NotSupportedException();
        }
    }
}
