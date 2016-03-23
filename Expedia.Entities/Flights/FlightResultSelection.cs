using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Flights
{
    public class FlightResultSelection : IFlightResult
    {
		public string DestinationName { get; set; }
		public string DestinationPictureUrl { get; set; }
		public string ReturnName { get; set; }

		// Used for storing the ICommand for resetting the selection.
		public object CustomData { get; set; }

		public FlightResultItem Flight { get; set; }

		public bool IsSelectable
		{
			get { return false; }
		}
	}
}
