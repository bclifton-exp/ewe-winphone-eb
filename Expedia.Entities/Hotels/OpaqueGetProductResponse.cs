using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class OpaqueGetProductResponse
    {
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }
		public int AdultCount { get; set; }
		public string NumberOfNights { get; set; }
		public string NumberOfRoomsRequested { get; set; }
		//public MobileError[] Errors { get; set; }
		public OpaqueHotelSearchResponse HotelRoomResponse { get; set; }
    }
}
