using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Hotels
{
    public class RoomOccupant
    {
		public int NumberOfAdultGuests { get; set; }
		public int[] ChildGuestsAge { get; set; }
    }
}
