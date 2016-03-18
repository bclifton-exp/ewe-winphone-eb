using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class Airport
	{
		public string AirportCode { get; set; }
		public string RegionId { get; set; }
		public string Name { get; set; }
		public string Country { get; set; }
	}
}
