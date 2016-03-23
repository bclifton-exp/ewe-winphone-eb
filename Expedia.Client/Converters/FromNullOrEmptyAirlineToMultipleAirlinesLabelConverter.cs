using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class FromNullOrEmptyAirlineToMultipleAirlinesLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null && (!string.IsNullOrWhiteSpace((string)value) || targetType != typeof(string)))
            {
                return value;
            }

            try
            {
                return "Multiple Airlines"; // TODO: Take the string from localized resources
            }
            catch (Exception exception)
            {
                //this.Log().Error("Unable to use the language's GenericCulture to create a RegionInfo.", exception);
            }

            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
