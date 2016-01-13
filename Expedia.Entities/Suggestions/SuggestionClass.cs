using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Suggestions
{
	public enum SuggestionClass
	{
		City = 0,
		Hotel = 2,
		Airport = 3,
		Region = 4,
		Country = 5,
		CurrentLocation = 6,
		Attraction = 7,

		[EditorBrowsable(EditorBrowsableState.Never)]
		CITY = 0,

		[EditorBrowsable(EditorBrowsableState.Never)]
		HOTEL = 1,

		[EditorBrowsable(EditorBrowsableState.Never)]
		AIRPORT = 2,

		[EditorBrowsable(EditorBrowsableState.Never)]
		ATTRACTION = 7,
	}
}
