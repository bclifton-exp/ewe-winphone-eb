using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class TransmissionTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value.ToString().Contains("AUTOMATIC"))
                return "Automatic";

            if (value.ToString().Contains("MANUAL"))
                return "Manual";

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
