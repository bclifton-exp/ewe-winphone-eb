using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class FlightDetailsResponse
	{
		public FlightOffer Offer { get; set; }
		public string ProductKey { get; set; }
		public FlightRules[] FlightRules { get; set; }
		public FlightOffer OldOffer { get; set; }
		public double PriceChangeAmount { get; set; }
		public MobilePrice ChangedPrice { get; set; }
		public double ObFeeTotalAmount { get; set; }
		public MobilePrice ObFeePrice { get; set; }
		public FlightLeg[] Legs { get; set; }

	}
}
