using System;

namespace Expedia.Entities.Hotels
{
	public class HotelResultItem
	{
		public string ImageUrl { get; set; }
		public int Price { get; set; }
		public int StrikeThroughPrice { get; set; }
		public bool StrikeThroughPriceIsHigher { get { return StrikeThroughPrice - Price > 0; } }
		public string HotelName { get; set; }
		public string RateCurrencyCode { get; set; }
		public string RateCurrencySymbol { get; set; }
		public int RoomsLeftAtThisRate { get; set; }
		public double Rating { get; set; }
        public double GuestRating { get; set; }
		public bool IsSoldOut { get { return RoomsLeftAtThisRate == 0 ; } }
		public string HotelId { get; set; }
        public bool IsVipAccess { get; set; }
        public string DiscountPercent { get; set; }

        public string LocationId { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

		public HotelResultItem()
		{
			
		}
	}
}