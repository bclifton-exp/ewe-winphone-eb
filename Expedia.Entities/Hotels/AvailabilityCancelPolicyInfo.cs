namespace Expedia.Entities.Hotels
{
	public class AvailabilityCancelPolicyInfo
	{
		public string VersionId { get; set; }
		public string CancelTime { get; set; }
		public string StartWindowHours { get; set; }
		public string NightCount { get; set; }
		public string Percent { get; set; }
		public string Amount { get; set; }
		public string CurrencyCode { get; set; }
		public string TimeZoneDescription { get; set; }
	}
}