using System;
using System.Collections.Generic;

namespace Expedia.Entities
{
	public class ExpediaClientException : Exception
	{
		public IEnumerable<ResponseError> Errors { get; private set; }

		public ExpediaClientException(IEnumerable<ResponseError> errors)
		{
			Errors = errors;
		}

		public override string ToString()
		{
			var error = string.Empty;
			//Errors.ForEach(e => e.SelectOrDefault(er => error += er.ErrorCode + ": " + er.ErrorMessage + ", "));
			return error;
		}
	}
}