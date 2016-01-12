using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
	public class GetProductResponse
	{
		//public MobileError[] Errors { get; set; }
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }
		public int AdultCount { get; set; }
		public string NumberOfNights { get; set; }
		public string NumberOfRooms { get; set; }
		public string HotelId { get; set; }
		public string HotelName { get; set; }
		public string LocalizedHotelName { get; set; }
		public string NonLocalizedHotelName { get; set; }
		public string LargeThumbnailUrl { get; set; }
		public string ThumbnailUrl { get; set; }
		public string HotelAddress { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string HotelCity { get; set; }
		public string HotelStateProvince { get; set; }
		public string HotelCountry { get; set; }
		public string HotelStarRating { get; set; }
		public HotelOffer HotelRoomResponse { get; set; }
		public string SupplierType { get; set; }
		public string AccessibilityAmenities { get; set; }
		public string IsVipAccess { get; set; }
		public string TealeafTransactionId { get; set; }
		public string BigMapUrl { get; set; }
	}
}
