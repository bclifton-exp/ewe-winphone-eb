using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Expedia.Entities.Extensions;
using nVentive.Umbrella.Extensions;

namespace Expedia.Entities.Hotels
{
	public class GetProductQueryParameters : IQueryParameters
	{
		public string SourceType { get; set; }
		public string ProductKey { get; set; }
		public RoomOccupant[] RoomOccupants { get; set; }
		public bool OpaqueProduct { get; set; }

		public void AppendParameters(Action<string, string> appender)
		{
			if (!string.IsNullOrWhiteSpace(SourceType))
			{
				appender("sourceType", SourceType);
			}

			appender("productKey", ProductKey);

			if (RoomOccupants != null && RoomOccupants.Length > 0)
			{
				RoomOccupants.ForEach((i, o) =>
				{
					appender(string.Format(CultureInfo.InvariantCulture, "roomOccupants[{0}].numberOfAdultGuests", i),
							 o.NumberOfAdultGuests.ToString("D", CultureInfo.InvariantCulture));

					if (o.ChildGuestsAge == null || o.ChildGuestsAge.Length < 1)
					{
						return;
					}

					o.ChildGuestsAge.ForEach((j, c) => appender(
						string.Format(CultureInfo.InvariantCulture, "roomOccupants[{0}].childGuestAge", i),
						c.ToString("D", CultureInfo.InvariantCulture)));
				});
			}

			if (OpaqueProduct)
			{
				appender("opaqueProduct", OpaqueProduct.ToLowerCaseString());
			}
		}
	}
}
