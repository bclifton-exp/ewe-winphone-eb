using System.Collections.Generic;

namespace Expedia.Entities.Hotels
{
    public class HotelOfferResponse
    {
        public string HotelTagline { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public string NumberOfRoomsRequested { get; set; }
        public string NumberOfNights { get; set; }
        public string TimeZoneUtcOffsetMinutes { get; set; }
        public string ShortDescription { get; set; }
        public string DeepLinkUrl { get; set; }
        public List<HotelRoomResponse> HotelRoomResponse { get; set; }
        public string SignedMapUrl { get; set; }
        public string AirAttachExpirationTimeSeconds { get; set; }
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
        public List<HotelOverviewText> HotelOverviewText { get; set; }
        public string FirstHotelOverview { get; set; }
        public HotelAmenitiesText HotelAmenitiesText { get; set; }
        public HotelPoliciesText HotelPoliciesText { get; set; }
        public HotelFeesText HotelFeesText { get; set; }
        public bool IsVipAccess { get; set; }
        public string LocationDescription { get; set; }
        public string LocationId { get; set; }
        public List<Region> Regions { get; set; }
    }

    public class SurchargesForEntireStay
    {
        public string Amount { get; set; }
        public string Type { get; set; }
    }

    public class NightlyRatesPerRoom
    {
        public string Promo { get; set; }
        public string BaseRate { get; set; }
        public string Rate { get; set; }
    }

    public class ChargeableRateInfo
    {
        public string MaxNightlyRate { get; set; }
        public string AverageRate { get; set; }
        public string TaxStatusType { get; set; }
        public string SurchargeTotal { get; set; }
        public string SurchargeTotalForEntireStay { get; set; }
        public string AverageBaseRate { get; set; }
        public string NightlyRateTotal { get; set; }
        //public int DiscountPercent { get; set; }
        public string Total { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencySymbol { get; set; }
        public List<Surcharge> Surcharges { get; set; }
        public List<SurchargesForEntireStay> SurchargesForEntireStay { get; set; }
        public List<NightlyRatesPerRoom> NightlyRatesPerRoom { get; set; }
        public string PriceToShowUsers { get; set; }
        public string StrikethroughPriceToShowUsers { get; set; }
        public string StrikethroughPriceWithTaxesAndFeesToShowUsers { get; set; }
        public string TotalMandatoryFees { get; set; }
        public string TotalPriceWithMandatoryFees { get; set; }
        public string FormattedTotalPriceWithMandatoryFees { get; set; }
        public string UserPriceType { get; set; }
        public List<object> PriceAdjustments { get; set; }
        public string CheckoutPriceType { get; set; }
        public bool AirAttached { get; set; }
        public string RoomTypeCode { get; set; }
        public string RatePlanCode { get; set; }
        public bool ShowResortFeeMessage { get; set; }
        public bool ResortFeeInclusion { get; set; }
    }

    public class RateInfo
    {
        public bool PriceBreakdown { get; set; }
        public bool Promo { get; set; }
        public bool RateChange { get; set; }
        public ChargeableRateInfo ChargeableRateInfo { get; set; }
    }

    public class PayLaterOffer
    {
        public string ProductKey { get; set; }
        public string CancellationPolicy { get; set; }
        public string RateDescription { get; set; }
        public string RoomTypeDescription { get; set; }
        public string RoomLongDescription { get; set; }
        public string RoomThumbnailUrl { get; set; }
        public string SupplierType { get; set; }
        public bool RateChange { get; set; }
        public bool NonRefundable { get; set; }
        public bool HasFreeCancellation { get; set; }
        public bool GuaranteeRequired { get; set; }
        public bool DepositRequired { get; set; }
        public bool ImmediateChargeRequired { get; set; }
        public string CurrentAllotment { get; set; }
        public string PromoDescription { get; set; }
        public bool IsDiscountRestrictedToCurrentSourceType { get; set; }
        public bool IsSameDayDrr { get; set; }
        public string SmokingPreferences { get; set; }
        public string RateOccupancyPerRoom { get; set; }
        public List<BedType> BedTypes { get; set; }
        public RateInfo RateInfo { get; set; }
        public List<ValueAdd> ValueAdds { get; set; }
        public string QuotedOccupancy { get; set; }
        public string MinGuestAge { get; set; }
        public string RoomTypeCode { get; set; }
        public string RatePlanCode { get; set; }
        public bool IsPayLater { get; set; }
        public List<string> DepositPolicy { get; set; }
        public bool IsMemberDeal { get; set; }
    }

    public class HotelRoomResponse
    {
        public string ProductKey { get; set; }
        public string CancellationPolicy { get; set; }
        public string RateDescription { get; set; }
        public string RoomTypeDescription { get; set; }
        public string RoomLongDescription { get; set; }
        public string RoomThumbnailUrl { get; set; }
        public string SupplierType { get; set; }
        public bool RateChange { get; set; }
        public bool NonRefundable { get; set; }
        public bool HasFreeCancellation { get; set; }
        public bool GuaranteeRequired { get; set; }
        public bool DepositRequired { get; set; }
        public bool ImmediateChargeRequired { get; set; }
        public string CurrentAllotment { get; set; }
        public string PromoDescription { get; set; }
        public bool IsDiscountRestrictedToCurrentSourceType { get; set; }
        public bool IsSameDayDrr { get; set; }
        public string SmokingPreferences { get; set; }
        public string RateOccupancyPerRoom { get; set; }
        public List<BedType> BedTypes { get; set; }
        public RateInfo RateInfo { get; set; }
        public List<ValueAdd> ValueAdds { get; set; }
        public string QuotedOccupancy { get; set; }
        public string MinGuestAge { get; set; }
        public string RoomTypeCode { get; set; }
        public string RatePlanCode { get; set; }
        public bool IsPayLater { get; set; }
        public PayLaterOffer PayLaterOffer { get; set; }
        public bool IsMemberDeal { get; set; }
    }

    public class HotelFeesText
    {
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
