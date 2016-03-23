using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Flights
{
	public class FlightResultItem : IFlightResult
    {
		public string ProductKey { get; set; }

		public string AirlineName { get; set; }
		public string DepartureTime { get; set; }
		public string ArrivalTime { get; set; }
        public string Duration { get; set; }
		public DateTimeOffset DepartureTimeRaw { get; set; }
		public DateTimeOffset ArrivalTimeRaw { get; set; }

		public int LegDurationInDays { get; set; }

		public double Price { get; set; }
		public string CurrencyCode { get; set; }
		public string FormattedPrice { get; set; }
		public string FormattedWholePrice { get; set; }

		public bool IsMultipleAirlines { get; set; }
		public string[] ListOfSegments { get; set; }
		public string LastSegmentArrivalAirportCode { get; set; }

		public string LegId { get; set; }

		public bool IsSelectable 
		{
			get { return true; }
		}
	}
}
