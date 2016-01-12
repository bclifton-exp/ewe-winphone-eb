using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class PriceOption
    {
		public int MinPrice { get; set; }
		public int MaxPrice { get; set; }
		public int Count { get; set; }
    }
}
