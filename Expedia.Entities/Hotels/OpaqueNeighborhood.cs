namespace Expedia.Entities.Hotels
{
	public class OpaqueNeighborhood
	{
		public string OpaqueHoodId { get; set; }
		public string OpaqueHoodName { get; set; }
		public string OpaqueHoodDescription { get; set; }
		public string CityName { get; set; }
		public string ProvinceName { get; set; }
		public Coordinate CentroId { get; set; }
		public Coordinate[] OpaqueHoodShape { get; set; }
	}
}