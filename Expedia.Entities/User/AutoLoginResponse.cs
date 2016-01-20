using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Expedia.Entities.User
{
    public class AutoLoginResponse
    {
		[JsonProperty("status")]
		public string Status { get; set; }

		[JsonProperty("rewardsState")]
		public int RewardsState { get; set; }

		[JsonProperty("tlAcctSource")]
		public string AccountSource { get; set; }

		[JsonProperty("tlAcctState")]
		public string AccountState { get; set; }

		[JsonProperty("tlAcctType")]
		public string AccountType { get; set; }

		[JsonProperty("tlError")]
		public string Error { get; set; }

		[JsonProperty("tlLoginSuccess")]
		public bool LoginSuccess { get; set; }
    }
}
