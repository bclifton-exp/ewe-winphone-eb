using System;
using System.Collections.Generic;
using System.Text;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class SignInSignOutTextConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            bool val = bool.Parse(value.ToString());
            var rl = new ResourceLoader();

            return rl.GetString(val ? "SignOutHeader" : "SignInHeader");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
