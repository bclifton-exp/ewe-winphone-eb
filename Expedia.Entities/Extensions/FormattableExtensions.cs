using System;
using System.Globalization;

namespace Expedia.Entities.Extensions
{
	public static class FormattableExtensions
	{
		public static string ToStringInvariant<T>(this T number) where T : IFormattable
		{
			return number.ToString(null, CultureInfo.InvariantCulture);
		}
	}
}
