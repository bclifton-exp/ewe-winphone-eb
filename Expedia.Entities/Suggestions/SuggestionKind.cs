using System;
using System.Collections.Generic;
using System.Text;

namespace Expedia.Client.Contracts
{
	[Flags]
    public enum SuggestionKind
    {
		None				= 0x000,
		Airports			= 0x001,
		Cities				= 0x002,
		MultiCities			= 0x004,
		Neighborhood		= 0x008,
		PointsOfInterest	= 0x010,
		// This value is undocumented, but Expedia explicitly wants us to pass "255".
		UnknownValue		= 0x020,
		AirportMetroCodes	= 0x040,
		Hotels				= 0x080,
		MultiRegions		= 0x100,
		TrainStations		= 0x200,
		MetroStations		= 0x400,
    }
}
