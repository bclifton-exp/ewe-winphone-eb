using System;

namespace Expedia.Entities.Flights
{
	public class FlightDetailsQueryParameters : FlightsQueryParametersBase
	{
		public string ProductKey { get; set; }

		public override void AppendParameters(Action<string, string> appender)
		{
			appender("productKey", ProductKey);
			base.AppendParameters(appender);
		}
	}
}