using System.Collections.Generic;
using System.Linq;
namespace Expedia.Entities.Flights
{
	public class FlightResults
	{
		public FlightResultSelection Selection { get; set; }
		public FlightResultItem[] Flights { get; set; }

		public FlightFilter[] AirlineFilters { get; set; }
		public FlightFilter[] StopCountFilters { get; set; }
	}
}