using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Expedia.Entities
{
    public class UserProfile
    {
        [JsonProperty("tuid")]
        public string TravelUserId { get; set; }
        
        [JsonProperty("expUserId")]
        public string ExpUserId { get; set; }
        
        [JsonProperty("email")]
        public string Email { get; set; }
        
        [JsonProperty("firstNmae")]
        public string FirstName { get; set; }
        
        [JsonProperty("middleName")]
        public string MiddleName { get; set; }
        
        [JsonProperty("lastName")]
        public string LastName { get; set; }
        
        [JsonProperty("success")]
        public bool Success { get; set; }
        
        [JsonProperty("errors")]
        public string[] Errors { get; set; }
        
        [JsonProperty("detailedStatus")]
        public string DetailedStatusCode { get; set; }
        
        [JsonProperty("detailedStatusMsg")]
        public string DetailedStatusMessage { get; set; }
        
        [JsonProperty("activityId")]
        public string ActivityId { get; set; }
        
        //[JsonProperty("associatedTravelers")]
        //public string[] AssociatedTravelers { get; set; }
    }
}
