using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class FlightSearchResponse
	{
		public FlightLeg[] Legs { get; set; }
		public FlightOffer[] Offers { get; set; }
		public MobileError[] Errors { get; set; }
		public SearchCity[] SearchCities { get; set; }
		public string ObFeesDetails { get; set; }
		public string ActivityId { get; set; }
		public string PercentDelaysCancellationUrl { get; set; }
	}
}
