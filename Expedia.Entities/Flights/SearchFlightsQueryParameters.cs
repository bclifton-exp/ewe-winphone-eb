using System;
using System.Globalization;
using Expedia.Entities.Extensions;

namespace Expedia.Entities.Flights
{
	public class SearchFlightsQueryParameters : FlightsQueryParametersBase
	{
		public SearchFlightsQueryParameters()
		{
			MaxOfferCount = 1600;
			IccAndMerchantFareCheckoutAllowed = true;
		}

		public int MaxOfferCount { get; set; }
		public bool PrettyPrint { get; set; }
		public bool IccAndMerchantFareCheckoutAllowed { get; set; }
		public string CorrelationId { get; set; }
		public bool IsOneWay { get; set; }

		public override void AppendParameters(Action<string, string> appender)
		{
			base.AppendParameters(appender);
			appender("prettyPrint", PrettyPrint.ToLowerCaseString());
			appender("lccAndMerchantFareCheckoutAllowed", IccAndMerchantFareCheckoutAllowed.ToLowerCaseString());
			appender("correlationId", CorrelationId);
			appender("maxOfferCount", MaxOfferCount.ToString("D", CultureInfo.InvariantCulture));
		}
	}
}
