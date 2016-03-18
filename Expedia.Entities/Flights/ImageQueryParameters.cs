using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entities.Extensions;

namespace Expedia.Entities.Flights
{
    public class ImageQueryParameters : IQueryParameters
    {
		public string DestinationCode { get; set; }
		public int ImageWidth { get; set; }
		public int ImageHeight { get; set; }

		public virtual void AppendParameters(Action<string, string> appender)
		{
			appender("destinationCode", DestinationCode);
			appender("imageWidth", ImageWidth.ToStringInvariant());
			appender("imageHeight", ImageHeight.ToStringInvariant());
		}
    }
}
