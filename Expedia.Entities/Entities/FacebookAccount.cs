using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Contracts;

namespace Expedia.Business.Entities
{
    public class FacebookAccount
    {
		public string AccessToken { get; set; }
		public string Email { get; set; }
		public string Name { get; set; }
		public long UserId { get; set; }

		//public FacebookLinking Linking { get; set; }
	}
}
