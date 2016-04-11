using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class PassengerConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int intVal;
            int.TryParse(value.ToString(), out intVal);

            return intVal == 0 ? "?" : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
