using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class FlightLeg
	{
		public string LegId { get; set; }
		public string BaggageFeesUrl { get; set; }
		public FlightSegment[] Segments { get; set; }
	}
}
