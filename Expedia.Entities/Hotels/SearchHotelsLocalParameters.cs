using Expedia.Entities.Hotels;
using System;
using System.Collections.Generic;

namespace Expedia.Entities.Hotels
{
	public class SearchHotelsLocalParameters
	{
		public string LocationName { get; set; }

		public string ShortLocationName
		{
			get
			{
				return string.IsNullOrWhiteSpace(LocationName) || !LocationName.Contains("(")
						? LocationName
						: LocationName.Remove(LocationName.IndexOf("(", StringComparison.OrdinalIgnoreCase))
							.Trim(' ', ',', '.');
			}
		}

		public double? LocationLatitude { get; set; }
		public double? LocationLongitude { get; set; }
		public string LocationRegionId { get; set; }
		public string NearestAirportCode { get; set; }
		
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }

		public int RoomsCount { get; set; }
		public int AdultsCount { get; set; }
		public int[] ChildrenAges { get; set; }
		public int TotalGuestsCount { get{return AdultsCount + (ChildrenAges ?? new int[0]).Length;} }

		public SortHotelsByType SortBy { get; set; }

		public HotelNeighborhoodFilter[] NeighborhoodFilters { get; set; }
		public HotelPriceBucketFilter[] PriceFilters { get; set; }
		public HotelStarRatingFilter[] StarRatingFilters { get; set; }
		public HotelFilter[] AmenityFilters { get; set; }
		public HotelFilter[] AccessibilityFilters { get; set; }

		//public override int GetHashCode()
		//{
		//	return this.Equality().GetHashCode(Fields);
		//}

		//public override bool Equals(object obj)
		//{
		//	return this.Equality().Equal(obj, Fields);
		//}

		private static readonly Func<SearchHotelsLocalParameters, IEnumerable<object>> Fields = obj => new object[] 
		{ 
			obj.LocationName, 
			obj.LocationLatitude,
			obj.LocationLongitude,
			obj.LocationRegionId,
			obj.NearestAirportCode,
			obj.CheckInDate,
			obj.CheckOutDate,
			obj.RoomsCount,
			obj.AdultsCount
		};

		

	}
}