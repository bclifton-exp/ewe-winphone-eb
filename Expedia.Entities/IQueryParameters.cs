using System;

namespace Expedia.Entities
{
	public interface IQueryParameters
	{
		void AppendParameters(Action<string, string> appender);
	}
}
