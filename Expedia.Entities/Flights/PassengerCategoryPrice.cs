using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class PassengerCategoryPrice
	{
		public PassengerCategory PassengerCategory { get; set; }
		public MobilePrice TotalPrice { get; set; }
		public MobilePrice BasePrice { get; set; }
		public MobilePrice TaxesPrice { get; set; }
	}
}
