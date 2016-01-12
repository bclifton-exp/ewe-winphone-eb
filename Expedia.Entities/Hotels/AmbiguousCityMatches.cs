using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class AmbiguousCityMatches
    {
		public int NumberOfMatches { get; set; }
		public AmbiguousCityMatch[] CityList { get; set; }
    }
}
