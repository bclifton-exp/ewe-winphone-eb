using Newtonsoft.Json;

namespace Expedia.Entities
{
    public class ExpediaFaultResponse
    {
        public Fault Fault { get; set; }
    }

    public class Fault
    {
        public FaultDetail Detail { get; set; }
        public string FaultString { get; set; }
    }

    public class FaultDetail
    {
        [JsonProperty("errorcode")]
        public string Code { get; set; }
    }
}