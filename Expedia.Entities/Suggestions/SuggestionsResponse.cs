using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Expedia.Entities.Suggestions
{
	public class SuggestionsResponse : ResponseBase
	{
		[JsonProperty("q")]
		public string Query { get; set; }

		[JsonProperty("rid")]
		public Guid SuggestionsResponseId { get; set; }

		[JsonProperty("rc")]
		public string StatusCode { get; set; }

		[JsonProperty("sr")]
		public SuggestionResult[] Suggestions { get; set; }

        public List<List<SuggestionResult>> SortedSuggestionsList { get; set; }
    }
}
