using Newtonsoft.Json;

namespace Expedia.Entities
{
	public class ResponseHeader
	{
		public int StatusCode { get; set; }
		public string StatusMessage { get; set; }
	
		[JsonProperty("Error")]
		public ResponseError[] Errors { get; set; }
	}
}
