using Expedia.Entities.Flights;
using System;
using System.Collections.Generic;
using Windows.Devices.Geolocation;

namespace Expedia.Entities.Flights
{
	public class SearchFlightsLocalParameters
	{
		public bool IsOneWay { get; set; }
		public DateTimeOffset? DepartureDate { get; set; }
		public DateTimeOffset? ReturnDate { get; set; }
		public string DepartureAirportCode { get; set; }
		public string DepartureCityShortName { get; set; }
        public BasicGeoposition DepartureAirportPosition { get; set; }
        public BasicGeoposition ArrivalAirportPosition { get; set; }
		public string ArrivalAirportCode { get; set; }
		public string ArrivalCityShortName { get; set; }
		public string ArrivalCityShorterName 
		{ 
			get
			{ 
				return string.IsNullOrWhiteSpace(ArrivalCityShortName) || !ArrivalCityShortName.Contains("(")
										? ArrivalCityShortName
										: ArrivalCityShortName
											   .Remove(ArrivalCityShortName
													.IndexOf("(", StringComparison.OrdinalIgnoreCase))
											   .Trim(' ', ',', '.');
			} 
		}
		public int AdultsCount { get; set; }
		public int[] ChildrenAges { get; set; }

		public int ChildCount { get { return (ChildrenAges ?? new int[0]).Length; } }
		public int TotalTravelersCount { get { return AdultsCount + ChildCount; } }

		public OrderFlightsByType OrderBy { get; set; }

		public FlightFilter[] DepartureAirlineFilters { get; set; }
		public FlightFilter[] DepartureStopCountFilters { get; set; }

		public FlightResultItem SelectedDeparture { get; set; }

		public FlightFilter[] ReturnAirlineFilters { get; set; }
		public FlightFilter[] ReturnStopCountFilters { get; set; }

        public Geopoint SelectedMapCenter { get; set; }


        //public override int GetHashCode()
        //{
        //	return this.Equality().GetHashCode(Fields);
        //}

        //public override bool Equals(object obj)
        //{
        //	return this.Equality().Equal(obj, Fields);
        //}

        private static readonly Func<SearchFlightsLocalParameters, IEnumerable<object>> Fields = obj => new object[] 
		{
			obj.IsOneWay,
			obj.DepartureDate,
			obj.ReturnDate,
			obj.DepartureAirportCode,
			obj.DepartureCityShortName,
			obj.ArrivalAirportCode,
			obj.ArrivalCityShortName,
			obj.AdultsCount
		};

		
		
	}
}
