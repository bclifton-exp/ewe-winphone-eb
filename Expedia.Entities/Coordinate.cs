using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Expedia.Entities
{

	[DebuggerDisplay("Lat: {Latitude}, Lon: {Longitude}")]
	public class Coordinate
	{
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
