using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entities.User;

namespace Expedia.Business.Entities
{
    public class AuthenticationException : Exception
    {
		private readonly SignInResponse _response;

		public AuthenticationException()
		{
		}

		public AuthenticationException(SignInResponse response)
		{
			_response = response;
		}

		public override string Message
		{
			get
			{
				return (_response == null) 
					? base.Message
					: _response.ToString();
			}
		}
    }
}
