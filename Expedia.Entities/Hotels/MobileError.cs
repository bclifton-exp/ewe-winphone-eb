using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entities.Flights;

namespace Expedia.Entities.Hotels
{
    public class MobileError
    {
		public string ErrorCode { get; set; }		
		public MobileErrorInfo ErrorInfo { get; set; }
		public string DiagnosticId { get; set; }
		public string DiagnosticFullText { get; set; }
		public string ActivityId { get; set; }
    }
}
