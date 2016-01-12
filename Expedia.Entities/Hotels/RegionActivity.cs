using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
	public class RegionActivity
    {
		public int BookingCount { get; set; }
		public string RegionId { get; set; }
		public bool ValidBookingCount { get; set; }
		public bool ValidViewCount { get; set; }
		public int ViewCount { get; set; }
    }
}
