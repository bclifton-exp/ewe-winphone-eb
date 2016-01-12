namespace Expedia.Entities
{
	public abstract class ResponseBase
	{
		public string Error { get; set; }

		public bool IsError
		{
			get { return !string.IsNullOrWhiteSpace(Error); }
		}

		public bool IsSuccess
		{
			get { return !IsError; }
		}
	}
}
