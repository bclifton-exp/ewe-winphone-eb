using System;
using System.Globalization;
using System.Linq;
using Expedia.Entities.Extensions;
using Newtonsoft.Json;

namespace Expedia.Entities.Hotels
{
	public class HotelSearchQueryParameters : IQueryParameters
	{
		public PriceType PriceType { get; set; }
		public string City { get; set; }
		public string RegionId { get; set; }
		public double? Latitude { get; set; }
		public double? Longitude { get; set; }
		public string CorrelationId { get; set; }

		public bool FilterUnavailable { get; set; }
		public string FilterHotelName { get; set; }
		public string FilterInventoryType { get; set; }
		public int[] FilterPrice { get; set; }
		public int[] FilterPriceBuckets { get; set; }
		public int[] FilterStarRatings { get; set; }
		public int[] FilterAmenities { get; set; }
		public SortHotelsByType SortOrder { get; set; }
		public SortHotelsByType SortOrderFilter { get; set; }
		public int ResultsPerPage { get; set; }
		public int PageIndex { get; set; }
		public bool ForceV2Search { get; set; }

        public bool ReturnOpaqueHotels { get; set; }
		public string HotelGroupId { get; set; }
		public string AirAttachQualificationCode { get; set; }
		public bool EnableSponsoredListing { get; set; }
		public DateTimeOffset CheckInDate { get; set; }
		public DateTimeOffset CheckOutDate { get; set; }
		public int[] Room { get; set; }
		public int[] Room1 { get; set; }

		public void AppendParameters(Action<string, string> appender)
		{
			appender("priceType", PriceType.ToString());
			appender("city", City);

			if (!string.IsNullOrWhiteSpace(RegionId))
			{
				appender("regionId", RegionId);
			}

			if (Latitude.HasValue && Longitude.HasValue)
			{
				appender("latitude", Latitude.Value.ToString("F4", CultureInfo.InvariantCulture));
				appender("longitude", Longitude.Value.ToString("F4", CultureInfo.InvariantCulture));
			}

			if (!string.IsNullOrWhiteSpace(CorrelationId))
			{
				appender("correlationId", CorrelationId);
			}

			if (FilterUnavailable)
			{
				appender("filterUnavailable", FilterUnavailable.ToLowerCaseString());
			}

			if (!string.IsNullOrWhiteSpace(FilterHotelName))
			{
				appender("filterHotelName", FilterHotelName);
			}

			if (!string.IsNullOrWhiteSpace(FilterInventoryType))
			{
				appender("filterInventoryType", FilterHotelName);
			}

			if (FilterPrice != null && FilterPrice.Length >= 1 && FilterPrice.Length <= 2 && FilterPrice[0] >= 0)
			{
				appender("filterPrice", string.Join(",", FilterPrice));
			}

			if (FilterPriceBuckets != null && FilterPriceBuckets.Length == 4)
			{
				appender("filterPriceBuckets", string.Join(",", FilterPriceBuckets));
			}

			if (FilterStarRatings != null && FilterStarRatings.Length > 0)
			{
				appender("filterStarRatings", string.Join(",", FilterStarRatings));
			}

			if (FilterAmenities != null && FilterAmenities.Length > 0)
			{
				appender("filterAmenities", string.Join(",", FilterAmenities));
			}

			appender("sortOrder", SortOrder.ToString());
			appender("sortOrderFilter", SortOrderFilter.ToString());
			appender("resultsPerPage", ResultsPerPage.ToString());
			appender("pageIndex", PageIndex.ToString());
			appender("forceV2Search", ForceV2Search.ToLowerCaseString());
			appender("returnOpaqueHotels", ReturnOpaqueHotels.ToLowerCaseString());
			appender("sourceType", "mobileweb");

			if (!string.IsNullOrWhiteSpace(HotelGroupId))
			{
				appender("hgid", HotelGroupId);
			}

			if (!string.IsNullOrWhiteSpace(AirAttachQualificationCode))
			{
				appender("airAttachQualificationCode", AirAttachQualificationCode);
			}

			if (EnableSponsoredListing)
			{
				appender("enableSponsoredListings", EnableSponsoredListing.ToLowerCaseString());
			}

			if (CheckInDate != DateTimeOffset.MinValue)
			{
				appender("checkInDate", CheckInDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			}

			if (CheckOutDate != DateTimeOffset.MinValue)
			{
				appender("checkOutDate", CheckOutDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
			}

			if (Room1 != null && Room1.Length > 0)
			{
				appender("room1", string.Join(",", Room1));
			}

			if (Room != null && Room.Length > 0)
			{
				appender("room", string.Join(",", Room));
			}
		}
	}
}
