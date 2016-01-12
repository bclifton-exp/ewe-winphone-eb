using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Expedia.Entites.Shared
{
    public static class StringCleaner
    {
        public static string ScrubHtml(string stringToScrub)
        {
            return Regex.Replace(stringToScrub, @"<[^>]+>|&nbsp;", "").Trim();
        }
    }
}
