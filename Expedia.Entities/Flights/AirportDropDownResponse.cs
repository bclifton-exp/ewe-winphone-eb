using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class AirportDropDownResponse
	{
		public Airport[] Aiports { get; set; }
		public Route[] Routes { get; set; }
	}
}
