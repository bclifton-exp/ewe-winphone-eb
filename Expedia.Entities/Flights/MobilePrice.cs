using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class MobilePrice
	{
		public double Amount { get; set; }
		public string FormattedPrice { get; set; }
		public string FormattedWholePrice { get; set; }
		public string CurrencyCode { get; set; }
	}
}
