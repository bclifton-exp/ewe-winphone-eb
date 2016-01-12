using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities
{
	public static class Extensions
	{
		public static bool IsFeedback(this Uri uri)
		{
			return Constants.MatchFeedbackHostname.IsMatch(uri.OriginalString);
		}

		public static bool IsExpedia(this Uri uri)
		{
			return Constants.MatchExpediaHostname.IsMatch(uri.OriginalString);
		}

		public static string FindExpediaDomain(this Uri uri)
		{
			if (uri == null) return null;

			var m = Constants.MatchExpediaHostname.Match(uri.OriginalString);
			if (!m.Success || m.Groups.Count < 1)
				return null;

			return m.Groups[1].Value.ToLowerInvariant();
		}

		public static bool EndsWithFileExtension(this Uri uri)
		{
			return uri != null
				   && uri.Segments[uri.Segments.Length - 1].Contains('.');
		}

		public static bool IsFileExtensionWellKnown(this Uri uri)
		{
			return uri != null
				   && Constants.BypassList.Any(ext =>
					   uri.OriginalString.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
		}

		public static bool IsPlayMediaFileExtension(this Uri uri)
		{
			return uri != null
				   && Constants.PlayMediaFileExtensions.Any(ext =>
					   uri.LocalPath.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
		}

		public static bool IsLaunchScheme(this Uri uri)
		{
			return uri != null
				   && Constants.LaunchUriSchemes.Any(scheme =>
					   uri.Scheme.StartsWith(scheme, StringComparison.OrdinalIgnoreCase));
		}

		public static bool IsLaunchFileExtension(this Uri uri)
		{
			return uri != null
				   && Constants.LaunchFileExtensions.Any(ext =>
					   uri.OriginalString.EndsWith(ext, StringComparison.OrdinalIgnoreCase));
		}
	}
}
