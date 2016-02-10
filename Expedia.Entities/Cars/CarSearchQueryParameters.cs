using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Cars
{
    public class CarSearchQueryParameters : IQueryParameters
    {
        public string AirportCode { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime DropOffTime { get; set; }
        public string PickupLocationLat { get; set; }
        public string PickupLocationLon { get; set; }
        public int SearchRadius { get; set; }

        public void AppendParameters(Action<string, string> appender)
        {
            appender("airportCode", AirportCode);
            appender("pickupTime", PickupTime.ToString("yyyy-MM-ddTHH:mm:ss"));
            appender("dropOffTime", DropOffTime.ToString("yyyy-MM-ddTHH:mm:ss"));
            appender("pickupLocation.lat", PickupLocationLat);
            appender("pickupLocation.lon", PickupLocationLon);
            appender("searchRadius", SearchRadius.ToString());
        }
    }
}
