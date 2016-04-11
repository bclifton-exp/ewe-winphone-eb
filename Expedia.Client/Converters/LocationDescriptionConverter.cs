using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class LocationDescriptionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var location = value.ToString();
            return location.Contains(",") ? location.Split(',').First() : location;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
