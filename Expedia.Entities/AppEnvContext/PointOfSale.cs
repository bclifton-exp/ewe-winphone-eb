using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Text;

namespace Expedia.Entities.AppEnvContext
{
	public class PointOfSale
	{
		public string CountryId { get; set; }
		public string DisplayName { get; set; }
		public string HostName { get; set; }
		public string HelpUri { get; set; }
		public PointOfSaleCulture[] Cultures { get; set; }
		public string AboutUri { get; set; }
		public string PrivacyUri { get; set; }
		public string TermsOfUseUri { get; set; }
		public string DateFormatHotel { get; set; }
	}
}
