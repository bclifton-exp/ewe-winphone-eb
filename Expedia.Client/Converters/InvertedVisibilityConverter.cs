using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class InvertedVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Visibility visibility;
            Enum.TryParse(value.ToString(), out visibility);

            return visibility == Visibility.Collapsed ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
