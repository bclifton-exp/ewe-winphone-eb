using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class GetOfferResponse
    {
		public string HotelTagLine { get; set; }
		public string CheckInDate { get; set; }
		public string CheckOutDate { get; set; }
		public int NumberOfRoomsRequested { get; set; }
		public string NumberOfNights { get; set; }
		public string TripAdvisorRating { get; set; }
		public long TimeZoneUtcOffsetMinutes { get; set; }
		public string CheckInInstructions { get; set; }
		public string ShortDescription { get; set; }
		public HotelOffer[] HotelRoomResponse { get; set; }
		public string SignedMapUrl { get; set; }
		public string AirAttachExpirationTimSeconds { get; set; }
		//public MobileError[] Errors { get; set; }
		public string HotelId { get; set; }
		public string HotelName { get; set; }
		public string LocalizedHotelName { get; set; }
		public string NonLocalizedHotelName { get; set; }
		public string HotelAddress { get; set; }
		public string HotelCity { get; set; }
		public string HotelStateProvince { get; set; }
		public string HotelCountry { get; set; }
		public string Latitude { get; set; }
		public string Longitude { get; set; }
		public string LongDescription { get; set; }
		public Photo[] Photos { get; set; }
		public Amenity[] HotelAmenities { get; set; }
		public string HotelStarRating { get; set; }
		public string HotelStarCssClassName { get; set; }
		public string HotelGuestRating { get; set; }
		public string TotalRecommendations { get; set; }
		public string TotalReviews { get; set; }
		public string PercentRecommended { get; set; }
		public string TeleSalesNumber { get; set; }
		public string DesktopOverrideNumber { get; set; }
		public TextSection HotelRenovationText { get; set; }
		public TextSection HotelMandatoryFeesText { get; set; }
		public TextSection[] HotelOverviewText { get; set; }
		public string FirstHotelOverview { get; set; }
		public TextSection HotelAmenitiesText { get; set; }
		public TextSection HotelPoliciesText { get; set; }
		public TextSection HotelFeesText { get; set; }
		public bool IsVipAccess { get; set; }
		public string LocationDescription { get; set; }
		public string LocationId { get; set; }
		public Region[] Regions { get; set; }
		public string AllowedToDisplayRatingAsStars { get; set; }
    }
}
