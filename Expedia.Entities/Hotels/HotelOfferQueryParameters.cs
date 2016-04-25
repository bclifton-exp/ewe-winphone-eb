using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class HotelOfferQueryParameters : IQueryParameters
    {
        public string HotelId { get; set; }
        public DateTimeOffset CheckInDate { get; set; }
        public DateTimeOffset CheckOutDate { get; set; }
        public int[] Room { get; set; }
        public int[] Room1 { get; set; }

        public void AppendParameters(Action<string, string> appender)
        {
            if (!string.IsNullOrWhiteSpace(HotelId))
            {
                appender("hotelId", HotelId);
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
