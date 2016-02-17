using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Data;

namespace Expedia.Client.Converters
{
    public class GeolocationAccessStatusToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var accessStatus = value as GeolocationAccessStatus? ?? GeolocationAccessStatus.Unspecified;

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    return true;
                case GeolocationAccessStatus.Denied:
                    return false;
                case GeolocationAccessStatus.Unspecified:
                    return false;
                default:
                    return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            var boolVal = Boolean.Parse(value.ToString());

            return boolVal ? GeolocationAccessStatus.Allowed : GeolocationAccessStatus.Denied;
        }
    }
}
