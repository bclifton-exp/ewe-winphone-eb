using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace Expedia.Client.Converters
{
    public class IndexToPivotColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int index = int.Parse(parameter.ToString());
            int selectedIndex = int.Parse(value.ToString());

            return index == selectedIndex ? new SolidColorBrush(Colors.Gold) : new SolidColorBrush(Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
