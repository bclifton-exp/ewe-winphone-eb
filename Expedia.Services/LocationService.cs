using System;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Expedia.Services.Interfaces;

namespace Expedia.Services
{
    public class LocationService : ILocationService
    {
        private ISettingsService _settingsService;

        public LocationService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        private async Task<bool> CanUseLocation()
        {
            var currentAccess = _settingsService.GetUseLocationService();

            switch (currentAccess)
            {
                case GeolocationAccessStatus.Allowed:
                    return true;
                case GeolocationAccessStatus.Unspecified:
                    var accessStatus = await Geolocator.RequestAccessAsync();
                    _settingsService.SetUseLocationService(accessStatus);

                    return accessStatus == GeolocationAccessStatus.Allowed;
                case GeolocationAccessStatus.Denied:
                    return false;
                default:
                    return false;
            }
        }
            
        public async Task<Geoposition> GetCurrentLocation()
        {
            var canUseLocation = await CanUseLocation();

            if (canUseLocation)
            {
                var geoLocator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default };
                var position = await geoLocator.GetGeopositionAsync();

                return position;
            }
            return null;
        }

    }
}
