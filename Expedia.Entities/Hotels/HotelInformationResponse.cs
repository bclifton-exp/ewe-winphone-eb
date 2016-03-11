using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class HotelInformationResponse
    {
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
        public List<Photo> Photos { get; set; }
        public List<HotelAmenity> HotelAmenities { get; set; }
        public string HotelStarRating { get; set; }
        public string HotelStarRatingCssClassName { get; set; }
        public string HotelGuestRating { get; set; }
        public string TotalRecommendations { get; set; }
        public string TotalReviews { get; set; }
        public string PercentRecommended { get; set; }
        public string TelesalesNumber { get; set; }
        public bool DeskTopOverrideNumber { get; set; }
        public HotelMandatoryFeesText HotelMandatoryFeesText { get; set; }
        public List<HotelOverviewText> HotelOverviewText { get; set; }
        public string FirstHotelOverview { get; set; }
        public HotelAmenitiesText HotelAmenitiesText { get; set; }
        public HotelPoliciesText HotelPoliciesText { get; set; }
        public bool IsVipAccess { get; set; }
        public string LocationDescription { get; set; }
        public string LocationId { get; set; }
        public List<Region> Regions { get; set; }
        public string DeepLinkUrl { get; set; }
    }

    public class HotelAmenity
    {
        public string Id { get; set; }
        public string Description { get; set; }
    }

    public class HotelMandatoryFeesText
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class HotelOverviewText
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class HotelAmenitiesText
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }

    public class HotelPoliciesText
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
