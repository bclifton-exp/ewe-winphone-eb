using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
	public class HotelOffer
	{
		public string ProductKey { get; set; }
		public string CancellationPolicy { get; set; }
		public string Policy { get; set; }
		public string RateDescription { get; set; }
		public string RoomTypeDescription { get; set; }
		public string RoomLongDescription { get; set; }
		public string RoomThumbnailUrl { get; set; }
		public string SupplierType { get; set; }
		public string OtherInformation { get; set; }
		public bool RateChange { get; set; }
		public bool NonRefundable { get; set; }
		public bool HasFreeCancellation { get; set; }
		public string FreeCancellationWindowDate { get; set; }
		public bool GaranteeRequired { get; set; }
		public bool DepositRequired { get; set; }
		public bool ImmediateChargeRequired { get; set; }
		public string CurrentAllotment { get; set; }
		public string PromoId { get; set; }
		public string PromoDescription { get; set; }
		public string IsDescountRestrictedToCurrentSourceType { get; set; }
		public string IsSameDayDrr { get; set; }
		public string SmokingPreferences { get; set; }
		public string RateOccupancyPerRoom { get; set; }
		public BedType[] BedTypes { get; set; }
		public AvailabilityCancelPolicyInfo[] CancellationPolicyInfoList { get; set; }
		public HotelRateInfo RateInfo { get; set; }
		public ValueAdd[] ValueAdds { get; set; }
		public string QuotedOccupancy { get; set; }
		public string MinGuestAge { get; set; }
		public string TaxRate { get; set; }
		public bool IsPayLater { get; set; }
		public HotelOffer PayLaterOffer { get; set; }
	}
}
