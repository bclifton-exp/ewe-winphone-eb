using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Contracts;
using Newtonsoft.Json;

namespace Expedia.Entities.User
{
    public class FacebookAccount
    {
		public string AccessToken { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public string UserId { get; set; }
		public FacebookLinking Linking { get; set; }
	}

    public class FacebookAccountJson
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public string UserId { get; set; }
    }
}
