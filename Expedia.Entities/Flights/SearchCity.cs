using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class SearchCity
	{
		public string Code { get; set; }
		public string City { get; set; }
		public string Province { get; set; }
		public string Country { get; set; }
		public FlightSearchType SearchType { get; set; } 
	}
}
