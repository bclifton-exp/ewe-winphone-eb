using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class OpaqueHotelSearchResponse
    {
		public int TotalHotelCount { get; set; }
		public OpaqueHotel[] OpaqueHotelList { get; set; }
		public OpaqueNeighborhood[] OpaqueNeighborhoodList { get; set; }
		public string ErrorMessage { get; set; }
	}
}
