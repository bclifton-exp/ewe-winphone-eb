using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Expedia.Client.Converters
{
    public class RadioBackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var isChecked = bool.Parse(value.ToString());
            return isChecked
                ? Application.Current.Resources["AppLightGrayHighlightBrush"] as SolidColorBrush
                : new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
