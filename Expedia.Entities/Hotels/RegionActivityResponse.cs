namespace Expedia.Entities.Hotels
{
	public class RegionActivityResponse
	{
		public RegionActivity[] HotelProductActivityAtRegionLevelList { get; set; }
		public int RegionBookingResponseStatus { get; set; }
		public int RegionViewResponseStatus { get; set; }
	}
}