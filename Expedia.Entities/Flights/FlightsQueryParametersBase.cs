using System;
using System.Globalization;
using Expedia.Entities.Extensions;

namespace Expedia.Entities.Flights
{
	public abstract class FlightsQueryParametersBase : IQueryParameters
	{
		public DateTimeOffset? DepartureDate { get; set; }
		public DateTimeOffset? ReturnDate { get; set; }
		public string DepartureAirportCode { get; set; }
		public string ArrivalAirportCode { get; set; }
		public int NumberOfAdultTravelers { get; set; }
		public int[] ChildTravelersAge { get; set; }
		public bool InfantSeatingInLap { get; set; }

		public SortFlightsByType OrderBy { get; set; }

		public virtual void AppendParameters(Action<string, string> appender)
		{
			if (!string.IsNullOrEmpty(DepartureAirportCode))
			{
				appender("departureAirport", DepartureAirportCode);
			}

			if (!string.IsNullOrEmpty(ArrivalAirportCode))
			{
				appender("arrivalAirport", ArrivalAirportCode);
			}

			if (DepartureDate != null)
			{
				appender("departureDate", DepartureDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			}

			if (ReturnDate.HasValue && ReturnDate.Value != DateTimeOffset.MinValue)
			{
				appender("returnDate", ReturnDate.Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			}

			if (NumberOfAdultTravelers > 0)
			{
				appender("numberOfAdultTravelers", NumberOfAdultTravelers.ToString("D", CultureInfo.InvariantCulture));
			}

			if (ChildTravelersAge != null && ChildTravelersAge.Length > 0)
			{
				ChildTravelersAge.ForEach(age => appender("childTravelerAge", age.ToString("D", CultureInfo.InvariantCulture)));
			}

			if (InfantSeatingInLap)
			{
				appender("infantSeatingInLap", InfantSeatingInLap.ToLowerCaseString());
			}
		}
	}
}