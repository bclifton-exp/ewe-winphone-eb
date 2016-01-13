using System;
using Expedia.Entites.Shared;
using Expedia.Entities;
using Newtonsoft.Json;

namespace Expedia.Entities.Suggestions
{
	public class SuggestionResult
	{
		[JsonProperty("index")]
		public int Index { get; set; }

		[JsonProperty("gaiaId")]
		public string GaiaId { get; set; }

        [JsonProperty("hotelId")]
        public string HotelId { get; set; }

        [JsonProperty("regionNames")]
        public SuggestionRegionNames RegionNames { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("hierarchyInfo")]
        public SuggestionHierarchyInfo HierarchyInfo { get; set; }

        [JsonProperty("coordinates")]
        public SuggestionCoordinates Coordinates { get; set; }

        //      [JsonProperty("hotelId")]
        //      public string HotelId { get; set; }

        //      [JsonProperty("address")]
        //      public string Address { get; set; }

        public string Display
		{
			get { return string.IsNullOrEmpty(RegionNames.DisplayName) ? RegionNames.FullName : StringCleaner.ScrubHtml(RegionNames.DisplayName); }
		}

	    public string Id
	    {
	        get { return GaiaId; }
	    }

  //      private static readonly Func<SuggestionResult, object[]> EqualityFields = (o => new object[] { o.Id });

		//public override bool Equals(object obj)
		//{
		//	return this.Equality().Equal(obj, EqualityFields);
		//}

		public override int GetHashCode()
		{
		    return string.IsNullOrEmpty(Id) ? 0 : Id.GetHashCode();
		}
	}

    public class SuggestionRegionNames
    {
        [JsonProperty("shortName")]
        public string ShortName { get; set; }

        [JsonProperty("fullName")]
        public string FullName { get; set; }

        [JsonProperty("displayName")]
        public string DisplayName { get; set; }
    }

    public class SuggestionHierarchyInfo
    {
        [JsonProperty("airport")]
        public SuggestionAirport Airport { get; set; }

        [JsonProperty("country")]
        public SuggestionCountry Country { get; set; }
    }

    public class SuggestionAirport
    {
        [JsonProperty("airportCode")]
        public string AirportCode { get; set; } 

        [JsonProperty("metrocode")]
        public string MetroCode { get; set; }

        [JsonProperty("multicity")]
        public string MultiCity { get; set; }
    }

    public class SuggestionCountry
    {
        [JsonProperty("name")]
        public string Country { get; set; }

        [JsonProperty("isoCode2")]  
        public string CountryCode { get; set; } 

        [JsonProperty("isoCode3")]
        public string CountryShortName { get; set; }
    }

    public class SuggestionCoordinates
    {
        [JsonProperty("lat")]
        public string Latitude { get; set; }

        [JsonProperty("long")]
        public string Longitude { get; set; }
    }

    public class SuggestionResultV2
    {
        [JsonProperty("i")]
        public int Rank { get; set; }
            
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("gaiaId")]
        public string GaiaId { get; set; }

        [JsonProperty("s")]
        public string ShortName { get; set; }

        [JsonProperty("l")]
        public string LongName { get; set; }

        [JsonProperty("f")]
        public string FullName { get; set; }

        [JsonProperty("a")]
        public string AirportCode { get; set; }

        [JsonProperty("c")]
        public string Country { get; set; }

        [JsonProperty("cc")]
        public string CountryCode { get; set; }

        [JsonProperty("ccc")]
        public string CountryShortName { get; set; }

        [JsonProperty("t")]
        public SuggestionClass Class { get; set; }

        [JsonProperty("rt")]
        public string Type { get; set; }

        public string Display
        {
            get { return FullName; }
        }

        //private static readonly Func<SuggestionResultV2, object[]> EqualityFields = (o => new object[] { o.Id });

        //public override bool Equals(object obj)
        //{
        //    return this.Equals(obj);
        //    //return this.Equality().Equal(obj, EqualityFields);
        //}

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
