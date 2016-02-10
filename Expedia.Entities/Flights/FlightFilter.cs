using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Flights
{
    public class FlightFilter
    {
		public string Id { get; set; }
		public int Count { get; set; }
		public string Title { get; set; }
		public string FormattedLowestPrice { get; set; }

		/// <summary>
		/// If all the matching flights within this filter are
		/// already excluded by other active filters, IsFilterEnabled is False.
		/// </summary>
		public bool IsFilterEnabled { get; set; }
		
		public bool IsFilterChecked { get; set; }

		public FlightResultItem[] MatchingFlights { get; set; }
    }
}
