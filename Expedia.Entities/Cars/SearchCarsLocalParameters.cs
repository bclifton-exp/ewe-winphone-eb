using Expedia.Entities.Cars;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;

namespace Expedia.Entities.Cars
{
    public class SearchCarsLocalParameters
    {
        public string AirportCode { get; set; }
        public DateTime PickupTime { get; set; }
        public DateTime DropOffTime { get; set; }
        public string PickupLocationLat { get; set; }
        public string PickupLocationLon { get; set; }  
        public int SearchRadius { get; set; }
        public string PickupLocationFriendly { get; set; }
        //TODO separate dropoff locations when API is avail 

        public SortCarsByType SortBy { get; set; }
        public Geopoint SelectedMapCenter { get; set; }

        private static readonly Func<SearchCarsLocalParameters, IEnumerable<object>> Fields = obj => new object[]
        {
            obj.AirportCode,
            obj.PickupTime,
            obj.DropOffTime,
            obj.PickupLocationLat,
            obj.PickupLocationLon,
            obj.SearchRadius
        };
    }
}
