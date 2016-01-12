using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
	public class BaseRateInfo
	{
		public string MaxNightlyRate { get; set; }
		public string AverageRate { get; set; }
		public TaxStatusType TaxStatusType { get; set; }
		public string SurchargeTotal { get; set; }
		public string SurchargeTotalForEntireStay { get; set; }
		public string AverageBaseRate { get; set; }
		public string NightlyRateTotal { get; set; }
		public string DiscountPercent { get; set; }
		public string Total { get; set; }
		public string CurrencyCode { get; set; }
		public string CurrencySymbol { get; set; }
		public Surcharge[] Surcharges { get; set; }
		public Surcharge[] SurchargesForEntireStay { get; set; }
		public NightlyRate[] NightlyRatesPerRoom { get; set; }
		public string NightlyRatesSize { get; set; }
		public int PriceToShowUsers { get; set; }
		public string DepositAmountToShowUsers { get; set; }
		public string PriceRemainingAfterDepositToShowUsers { get; set; }
		public string DepositAmountExact { get; set; }
		public string PriceRemainingAfterDepositExact { get; set; }
		public int StrikethroughPriceToShowUsers { get; set; }
		public string StrikethroughPriceWithTaxesAndFeesToShowUsers { get; set; }
		public string TotalMandatoryFees { get; set; }
		public string TotalPriceWithMandatoryFees { get; set; }
		public UserPriceType UserPriceType { get; set; }
		public PriceAdjustment[] PriceAdjustments { get; set; }
		public string CheckoutPriceType { get; set; }
		public bool AirAttached { get; set; }
		public string RoomTypeCode { get; set; }
		public string RatePlan { get; set; }
		public bool ShowResortFeeMessage { get; set; }
		public bool ResortFeeInclusion { get; set; }
		public bool DailyMandatoryFee { get; set; }
	}
}
