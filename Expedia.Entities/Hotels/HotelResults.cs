using Expedia.Business.Entities;
using Expedia.Entities.Hotels;

namespace Expedia.Entities.Hotels
{
	public class HotelResults
	{
		public HotelResultItem[] Hotels { get; set; }

		public string SearchRegionId { get; set; }
		public int NumberOfRoomsRequested { get; set; }
		public int AvailableHotelCount { get; set; }
		public int TotalHotelCount { get; set; }

		public SortHotelsByType SortBy { get; set; }

		public bool FilterUnavailableHotelRequested { get; set; }
		public bool FilteredSearchMatchedNoHotels { get; set; }

		public HotelStarRatingFilter[] StarRatingFilters { get; set; }
		public HotelPriceBucketFilter[] PriceFilters { get; set; }
		public HotelNeighborhoodFilter[] NeighborhoodFilters { get; set; }
		public HotelFilter[] AmenityFilters { get; set; }
		public HotelFilter[] AccessibilityFilters { get; set; }

		public int PeopleShoppingInSameRegionCount { get; set; }
		public string PeopleShoppingInSameRegionName { get; set; }
	}
}