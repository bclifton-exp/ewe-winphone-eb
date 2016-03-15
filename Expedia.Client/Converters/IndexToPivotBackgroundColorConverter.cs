using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class IndexToPivotBackgroundColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int selectedIndex = int.Parse(value.ToString());

            switch (selectedIndex)
            {
                case 0:
                    return Application.Current.Resources["HotelGreen"];
                case 1:
                    return Application.Current.Resources["FlightBlue"];
                case 2:
                    return Application.Current.Resources["CarsPurple"];
                default:
                    return Application.Current.Resources["HotelGreen"];
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            return value;
        }
    }
}
