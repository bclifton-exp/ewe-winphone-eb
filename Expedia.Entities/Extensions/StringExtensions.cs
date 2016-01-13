using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Expedia.Entites
{
    public static class StringExtensions
    {
#if (!SILVERLIGHT && !METRO && HAS_COMPILEDREGEX) || WINDOWS_PHONE || HAS_COMPILEDREGEX
        private static readonly Regex _newLineRegex = new Regex(@"^", RegexOptions.Compiled | RegexOptions.Multiline);
#else
        private static readonly Regex _newLineRegex = new Regex(@"^", RegexOptions.Multiline);
#endif

		public static bool IsNullOrEmpty(this string instance)
		{
			return string.IsNullOrEmpty(instance);
		}

		public static bool IsNullOrWhiteSpace(this string instance)
		{
			return string.IsNullOrWhiteSpace(instance);
		}

		public static bool HasValue(this string instance)
		{
			return !string.IsNullOrWhiteSpace(instance);
		}

		public static bool HasValueTrimmed(this string instance)
		{
			return !string.IsNullOrWhiteSpace(instance);
		}

        public static bool IsNumber(this string instance)
        {
            return instance.Trim().ToCharArray().All(Char.IsNumber);
        }

		public static bool IsDigit(this string instance)
        {
            return instance.Trim().ToCharArray().All(Char.IsDigit);
        }

        public static string JoinBy(this IEnumerable<string> items, string joinBy)
        {
            return string.Join(joinBy, items.ToArray());
        }

        public static string InvariantCultureFormat(this string instance, params object[] array)
        {
             return string.Format(CultureInfo.InvariantCulture, instance, array);
        }

        public static string CurrentCultureFormat(this string instance, params object[] array)
        {
             return string.Format(CultureInfo.CurrentCulture, instance, array);
        }

		public static string Left(this string instance, int length)
		{
			return instance.LeftRightInternal(length, () => instance.Substring(0, length));
		}

		public static string Right(this string instance, int length)
		{
			return instance.LeftRightInternal(length, () => instance.Substring(instance.Length - length, length));
		}

		private static string LeftRightInternal(this string instance, int length, Func<string> predicate)
		{
			if(length < 0)
				throw new ArgumentException("'length' must be greater than zero.", "length");

			if(instance == null || length == 0)
				return string.Empty;

			// return whole value if string length is greater or equal than length parameter, otherwise invoke the result for the value.
			return length >= instance.Length ? instance : predicate.Invoke();
		}

        public static string Append(this string target, string chunk)
        {
            return target.Append(chunk, s => true);
        }

        public static string Append(this string target, string chunk, Func<string, bool> condition)
        {
            return target + (condition(target) ? chunk : "");
        }

        public static string AppendIfMissing(this string target, string chunk)
        {
            return target.Append(chunk, s => !s.EndsWith(chunk, StringComparison.Ordinal));
        }

		public static string TrimStart(this string source, string trimText)
		{
			if (source.StartsWith(trimText, StringComparison.Ordinal))
			{
				return source.Substring(trimText.Length).TrimStart(trimText, StringComparison.Ordinal);
			}
			else
			{
				return source;
			}
		}

		public static string TrimStart(this string source, string trimText, StringComparison comparisonType)
		{
			if (source.StartsWith(trimText, comparisonType))
			{
				return source.Substring(trimText.Length).TrimStart(trimText, comparisonType);
			}
			else
			{
				return source;
			}
		}

		public static string TrimEnd(this string source, string trimText)
		{
			if (source.EndsWith(trimText, StringComparison.Ordinal))
			{
				return source.Substring(0, source.Length - trimText.Length).TrimEnd(trimText, StringComparison.Ordinal);
			}
			else
			{
				return source;
			}
		}

		public static string TrimEnd(this string source, string trimText, StringComparison comparisonType)
		{
			if (source.EndsWith(trimText, comparisonType))
			{
				return source.Substring(0, source.Length - trimText.Length).TrimEnd(trimText, comparisonType);
			}
			else
			{
				return source;
			}
		}

		public static string Indent(this string text, int indentCount = 1)
		{
			return _newLineRegex.Replace(text, new String('\t', indentCount));
		}
	}
}
