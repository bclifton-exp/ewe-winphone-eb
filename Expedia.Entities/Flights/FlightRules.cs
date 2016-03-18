using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class FlightRules
	{
		public bool IsChangeAllowed { get; set; }
		public bool IsEnrouteChangeAllowed { get; set; }
		public bool IsEnrouteRefundAllowed { get; set; }
		public bool IsRefundable { get; set; }
		public string CurrencyCode { get; set; }
		public double ChangePenaltyAmount { get; set; }
		public double RefundPenaltyAmount { get; set; }
		public double EnrouteChangePenaltyAmount { get; set; }
		public double EnrouteRefundAllowedAmount { get; set; }
		public MobilePrice ChangePenaltyPrice { get; set; }
		public MobilePrice RefundPenaltyPrice { get; set; }
		public MobilePrice EnrouteChangePenaltyPrice { get; set; }
		public MobilePrice EnrouteRefundAllowedPrice { get; set; }
		public string SummaryText { get; set; }
 	}
}
