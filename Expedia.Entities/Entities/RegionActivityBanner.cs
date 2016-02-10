using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Business.Entities
{
	public class RegionActivityBanner
    {
	    public string RegionId { get; set; }
		public string RegionName { get; set; }
		public int OtherPeopleShoppingInSameRegion { get; set; }

		public bool ShowBanner { get; set; }
		public string Message { get; set; }
    }
}
