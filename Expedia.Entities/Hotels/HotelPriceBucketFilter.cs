using Expedia.Entities.Hotels;

namespace Expedia.Entities.Hotels
{
	public class HotelPriceBucketFilter : HotelFilter
	{
		public int MinPrice { get; set; }
		public int MaxPrice { get; set; }

		public string CurrencyCode { get; set; }
		public string CurrencySymbol { get; set; }
	}
}