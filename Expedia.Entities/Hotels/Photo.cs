using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class Photo
    {
		public string DisplayText { get; set; }
		public string ThumbnailUrl { get; set; }
		public string Url { get; set; }
		public bool Featured { get; set; }
    }
}
