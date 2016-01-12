namespace Expedia.Entities.Hotels
{
	public class OpaqueHotel
	{
		public string SortIndex { get; set; }
		public string HotelName { get; set; }
		public string HotelStarRating { get; set; }
		public string OpaqueHoodId { get; set; }
		public string OpaqueSearchResultId { get; set; }
		public OpaqueNeighborhood OpaqueNeighborhood { get; set; }
		public string ProductKey { get; set; }
		public string[] HotelAmenities { get; set; }
		public HotelRateInfo HotelOffer { get; set; }
	}
}