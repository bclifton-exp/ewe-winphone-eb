using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Expedia.Entities.User
{
    public class LinkAccountResponse
    {
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("rewardsState")]
		public int RewardsState { get; set; }

		[JsonProperty("tlAccountCreateSuccess")]
		public bool IsCreateSuccessful { get; set; }

		[JsonProperty("tlAcctSource")]
		public string AccountSource { get; set; }

		[JsonProperty("tlAcctState")]
		public string AccountState { get; set; }

		[JsonProperty("tlAcctType")]
		public string AccountType { get; set; }

		//[JsonProperty("tlError")]
		//public string Error { get; set; }

		[JsonProperty("tlLoginSuccess")]
		public bool LoginSuccess { get; set; }

		[JsonProperty("tlEMail")]
		public string Email { get; set; }

		[JsonProperty("tlTUID")]
		public long Tuid { get; set; }

	}
}
