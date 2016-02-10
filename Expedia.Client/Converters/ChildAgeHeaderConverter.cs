using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.ApplicationModel.Resources.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class ChildAgeHeaderConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var rl = new ResourceLoader();
            var text = rl.GetString("ChildAgeHeader");

            return string.Format(text, value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
