using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class FlightOffer
	{
		public string ProductKey { get; set; }
		public string[] LegIds { get; set; }
		
		public string Currency { get; set; }
		public string BaseFare { get; set; }
		public MobilePrice BaseFarePrice { get; set; }
		public string TotalFare { get; set; }
		public MobilePrice TotalFarePrice { get; set; }
		public PassengerCategoryPrice[] PricePerPassengerCategory { get; set; }
		public MobilePrice AverageTotalPricePerTicket { get; set; }
		public int NumberOfTickets { get; set; }
		public string Taxes { get; set; }
		public MobilePrice TaxesPrice { get; set; }
		public string Fees { get; set; }
		public MobilePrice FeesPrice { get; set; }
		public bool ShowFees { get; set; }
		public string MobileShoppingKey { get; set; }
		public int SeatsRemaining { get; set; }
		
		// TODO: How to generate a serializer for a 2 dimensional array? 
		// public FlightSegmentAttributes[][] SegmentAttributes { get; set; }
		
		public string BaggageFeesUrl { get; set; }
		public string FareType { get; set; }
		public string FareName { get; set; }
		public string FareNameLink { get; set; }

		public bool IsInternational { get; set; }
		public bool MayChangeObFees { get; set; }
		public bool HasBagFee { get; set; }
		public bool HasNoBagFee { get; set; }
	}
}
