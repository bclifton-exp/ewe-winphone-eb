using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class NullableBoolToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            bool inverse = false;
            if (parameter != null)
            {
				var s = parameter.ToString().ToUpper();

                inverse = s == "I" || s == "INVERSE";
            }

            Visibility visibilityOnTrue = (!inverse) ? Visibility.Visible : Visibility.Collapsed;
            Visibility visibilityOnFalse = (!inverse) ? Visibility.Collapsed : Visibility.Visible;

            var valueToConvert = value != null && System.Convert.ToBoolean(value, CultureInfo.InvariantCulture);

            return valueToConvert ? visibilityOnTrue : visibilityOnFalse;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            throw new NotSupportedException();
        }
    }
}
