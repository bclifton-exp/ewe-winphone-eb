using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities.Flights
{
	public class MobileError
	{
		public string ErrorCode { get; set; }
		public MobileErrorInfo ErrorInfo { get; set; }
		public string DiagnosticId { get; set; }
	}
}
