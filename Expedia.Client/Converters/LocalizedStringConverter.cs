using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class LocalizedStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, String language)
        {
            string locKey = parameter as string;
            string formatString = GetLocString(locKey);
            string output = String.Format(System.Globalization.CultureInfo.CurrentCulture, formatString, value);
            return output;
        }

        public object ConvertBack(object value, Type targetType, object parameter, String language)
        {
            throw new NotSupportedException();
        }

        private string GetLocString(string locKey)
        {
            var loader = Windows.ApplicationModel.Resources.ResourceLoader.GetForCurrentView();
            string locStr = loader.GetString(locKey);
            return locStr;
        }
    }
}
