using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class MinimumCarDoorsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int actualValue;
            int.TryParse(value.ToString(), out actualValue);

            return actualValue == 0 ? 2 : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
