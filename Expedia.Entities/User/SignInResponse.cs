using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entites;
using Newtonsoft.Json;

namespace Expedia.Entities.User
{
    public class SignInResponse
    {
		[JsonProperty("tuid")]
		public long TUid { get; set; }

		[JsonProperty("expUserId")]
		public long ExpediaUserId { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("firstName")]
		public string FirstName { get; set; }

		[JsonProperty("middleName")]
		public string MiddleName { get; set; }

		[JsonProperty("lastName")]
		public string LastName { get; set; }

		[JsonProperty("success")]
		public bool IsSuccess { get; set; }

		[JsonProperty("errors")]
		public MobileError Errors { get; set; }

		[JsonProperty("detailedStatus")]
		public string DetailedStatus { get; set; }

		[JsonProperty("detailedStatusMsg")]
		public string DetailedStatusMessage { get; set; }

		//[JsonProperty("phoneNumbers")]
		//public Phone[] PhoneNumbers { get; set; }

		public override string ToString()
		{
			return "Status={0}, Message={1}\nError: {2}".InvariantCultureFormat(
				DetailedStatus,
				DetailedStatusMessage,
				(Errors == null) ? "--" : Errors.ToString());
		}
	}
}
