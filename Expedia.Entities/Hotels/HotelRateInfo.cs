namespace Expedia.Entities.Hotels
{
	public class HotelRateInfo
	{
		public bool PriceBreakdown { get; set; }
		public bool Promo { get; set; }
		public bool RateChange { get; set; }
		public BaseRateInfo ChargeableRateInfo { get; set; }
		public BaseRateInfo ConvertedRateInfo { get; set; }
	}
}