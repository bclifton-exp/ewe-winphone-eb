using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class StringFormatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
			var currentCulture = string.IsNullOrWhiteSpace(culture) ? CultureInfo.CurrentUICulture : new CultureInfo(culture);

            if (parameter == null)
            {
                //this.Log().Warn("You didn't specified the pattern to use for a string format converter. You should specified it using the ConverterParameter.");
                parameter = string.Empty;
            }

            return String.Format(currentCulture, parameter.ToString(), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotSupportedException();
        }
    }
}
