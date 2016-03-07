using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Client.Contracts;

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
}
