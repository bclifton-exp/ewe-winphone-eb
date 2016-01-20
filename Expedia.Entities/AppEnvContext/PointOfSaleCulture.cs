using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.AppEnvContext
{
	public class PointOfSaleCulture
	{
		// The code used to create the matching CultureInfo
		public string CultureCode { get; set; }
		// The readable language name
		public string DisplayName { get; set; }
		// For hosts that support more than one language, that's the ID to pass as a parameter
		public string LanguageId { get; set; }
		// TODO
		public string LanguageCode { get; set; }
	}
}
