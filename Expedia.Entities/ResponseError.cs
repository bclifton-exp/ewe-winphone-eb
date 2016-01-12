using Newtonsoft.Json;

namespace Expedia.Entities
{
	public class ResponseError
	{
		private int? _errorCode = null;

		[JsonProperty("ErrorCode")]
		public string ErrorCodeText { get; set; }
		public string ErrorMessage { get; set; }

		[JsonIgnore]
		public int ErrorCode
		{
			get
			{
				if (_errorCode.HasValue)
				{
					return _errorCode.Value;
				}

				int errorCode;
				if (!int.TryParse(ErrorCodeText, out errorCode))
				{
					errorCode = -1;
				}
				_errorCode = errorCode;
				return errorCode;
			}
		}
	}
}
