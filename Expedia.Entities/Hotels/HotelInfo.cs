using System;

namespace Expedia.Entities.Hotels
{
	public class HotelInfo
	{
		public string HotelCode { get; set; }
		public string HotelName { get; set; }
		public string HotelPhone { get; set; }
		public string Brand { get; set; }
		public string CountryFullName { get; set; }
		public string RegionFullName { get; set; }
		public Coordinate Coordinate { get; set; }
		public Uri BrandIcon { get; set; }
		public Uri BrandLogo { get; set; }
		public double GMTHours { get; set; }
	}
}
