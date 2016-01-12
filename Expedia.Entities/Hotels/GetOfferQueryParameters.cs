using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Expedia.Entities.Extensions;

namespace Expedia.Entities.Hotels
{
    public class GetOfferQueryParameters : IQueryParameters
    {
		public string HotelId { get; set; }
		public string PriceType { get; set; }
		public bool UseCacheForAirAttach { get; set; }
		public string SourceType { get; set; }
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }
		public int[] Room1 { get; set; }
		public int[] Room { get; set; }


	    public void AppendParameters(Action<string, string> appender)
	    {
		    appender("hotelId", HotelId);

		    if (!string.IsNullOrWhiteSpace(PriceType))
		    {
			    appender("priceType", PriceType);
		    }

		    if (UseCacheForAirAttach)
		    {
			    appender("useCacheForAirAttach", UseCacheForAirAttach.ToLowerCaseString());
		    }

		    if (!string.IsNullOrWhiteSpace(SourceType))
		    {
			    appender("sourceType", SourceType);
		    }

		    if (CheckInDate != DateTimeOffset.MinValue)
		    {
			    appender("checkInDate", CheckInDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
		    }

		    if (CheckOutDate != DateTimeOffset.MinValue)
		    {
			    appender("checkOutDate", CheckOutDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
		    }

		    if (Room1 != null && Room1.Length > 0)
		    {
			    appender("room1", string.Join(",", Room1));
		    }

		    if (Room != null && Room.Length > 0)
		    {
			    appender("room", string.Join(",", Room));
		    }
	    }
    }
}
