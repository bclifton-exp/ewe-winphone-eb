using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class FlightSegment
	{
		public string DepartureTime { get; set; }
		public long DepartureTimeEpochSeconds { get; set; }
		public DateTimeOffset DepartureTimeRaw { get; set; }
		public long DepartureTimeZoneOffsetSeconds { get; set; }
		
		public string ArrivalTime { get; set; }
		public long ArrivalTimeEpochSeconds { get; set; }
		public DateTimeOffset ArrivalTimeRaw { get; set; }
		public long ArrivalTimeZoneOffsetSeconds { get; set; }

		public string ArrivalAirportCode { get; set; }
		public string ArrivalAirportLocation { get; set; }
		public string DepartureAirportCode { get; set; }
		public string DepartureAirportLocation { get; set; }

		public string AirlineName { get; set; }
		public string AirlineCode { get; set; }
		public string FlightNumber { get; set; }
		public string OnTimePercentage { get; set; }
		public string OperatingAirlineName { get; set; }
		public string OperatingAirlineCode { get; set; }
		public string EquipmentCode { get; set; }
		public string EquipmentDescription { get; set; }
		public string Duration { get; set; }
		public long Distance { get; set; }
		public string DistanceUnits { get; set; }
		public int Stops { get; set; }
		public string Meal { get; set; }
		public bool SameFlightAsPreviousSegment { get; set; }
		public string ProviderCode { get; set; }
	}
}
