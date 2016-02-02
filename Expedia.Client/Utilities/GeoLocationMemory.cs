using Windows.Devices.Geolocation;

namespace Expedia.Client.Utilities
{
    public class GeoLocationMemory
    {
        private static GeoLocationMemory _instance;
        private Geoposition Geoposition { get; set; }

        protected GeoLocationMemory()
        {

        }

        public static GeoLocationMemory Instance()
        {
            return _instance ?? (_instance = new GeoLocationMemory());
        }

        public void SetGeoposition(Geoposition geoposition)
        {
            Geoposition = geoposition;
        }

        public Geoposition GetCurrentGeoposition()
        {
            return Geoposition;
        }
    }
}
