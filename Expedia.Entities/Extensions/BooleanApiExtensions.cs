using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Extensions
{
    public static class BooleanApiExtensions
    {
	    private const string TrueString = "true";
	    private const string FalseString = "false";

		/// <summary>
		/// Returns 'true' or 'false' in lowercase.
		/// </summary>
		/// <param name="val">Boolean value to convert to a string.</param>
		/// <returns></returns>
	    public static string ToLowerCaseString(this bool val)
	    {
		    return val ? TrueString : FalseString;
	    }
    }
}
