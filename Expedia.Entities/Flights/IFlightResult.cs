using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Entities.Flights
{
    public interface IFlightResult
    {
		bool IsSelectable { get; }
    }
}
