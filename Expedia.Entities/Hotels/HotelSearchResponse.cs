using System.Collections.Generic;

namespace Expedia.Entities.Hotels
{
	public class HotelSearchResponse
	{
		public int NumberOfRoomsRequested { get; set; }
		public bool FilterUnavailableHotelsRequested { get; set; }
		public int TotalHotelCount { get; set; }
		public int AvailableHotelCount { get; set; }
		public string SearchRegionId { get; set; }
		public bool FilteredSearchMatchedNoHotels { get; set; }
		public OpaqueHotelSearchResponse OpaqueHotelResponse { get; set; }
		public Neighborhood[] AllNeighborhoodsInSearchRegion { get; set; }
		public StarOption[] StarOptions { get; set; }
		public PriceOption[] PriceOptions { get; set; }
		public IDictionary<string, AmenityFilterOption> AmenityFilterOptions { get; set; } 

		public Hotel[] HotelList { get; set; }
	}
}