using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class Hotel
    {
		public string SortIndex { get; set; }
		public string HotelId { get; set; }
		public string Name { get; set; }
		public string LocalizedName { get; set; }
		public string NonLocalizedName { get; set; }
		public string Address { get; set; }
		public string City { get; set; }
		public string StateProvince { get; set; }
		public string CountryCode { get; set; }
		public string PostalCode { get; set; }
		public string AirportCode { get; set; }
		public SupplierType SupplierType { get; set; }
		public double HotelStarRating { get; set; }
		public string HotelStarRatingCssClassName { get; set; }
		public double HotelGuestRating { get; set; }
		public int TotalRecommendations { get; set; }
		public string PercentRecommended { get; set; }
		public int TotalReviews { get; set; }
		public string ShortDescription { get; set; }
		public string LocationDescription { get; set; }
		public string LocationId { get; set; }
		public string LowRate { get; set; }
		public BaseRateInfo LowRateInfo { get; set; }
		public string RateCurrencyCode { get; set; }
		public string RateCurrencySymbol { get; set; }
		public int RoomsLeftAtThisRate { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
		public string ProximityDistanceInMile { get; set; }
		public string ProximityDistanceInKiloMeters { get; set; }
		public string LargeThumbnailUrl { get; set; }
		public string ThumbnailUrl { get; set; }
		public string DiscountMessage { get; set; }
		public bool IsDiscountRestrictedToCurrentSourceType { get; set; }
		public bool IsSameDayDrr { get; set; }
		public bool IsHotelAvailable { get; set; }
		public string NoAvailableMessage { get; set; }
		public bool IsSponsoredListing { get; set; }
		public string ClickTrackingUrl { get; set; }
		public string ImpressionTrackingUrl { get; set; }
		public string OmnitureAdDisplayedUrl { get; set; }
		public string OmnitureAdClickedUrl { get; set; }
		public bool HasFreeCancellation { get; set; }
		public Amenity[] Amenities { get; set; }
		public string DistanceUnit { get; set; }
		public bool DidGetBackHighestPriceFromSurvey { get; set; }
		public string HighestPriceFromSurvey { get; set; }
		public bool IsDudley { get; set; }
		public bool IsVipAccess { get; set; }
		public bool IsPaymentChoiceAvailable { get; set; }
		public bool IsShowEtpChoice { get; set; }
		public bool AllowedToDisplayRatingAsStars { get; set; }
	}
}
