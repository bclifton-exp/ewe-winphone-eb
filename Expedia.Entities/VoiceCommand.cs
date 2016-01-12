using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expedia.Entities
{
    public class VoiceCommand
    {
        public VoiceCommand()
        {
            Parameters = new Dictionary<string, string>();
        }

        public VoiceCommandType CommandType { get; set; }
        public IDictionary<string, string> Parameters { get; private set; }
    }
}
