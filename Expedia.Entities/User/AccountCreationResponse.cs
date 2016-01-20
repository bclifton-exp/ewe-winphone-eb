using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Expedia.Entities.User
{
    public class AccountCreationResponse
    {
        [JsonProperty("tuid")]
        public string TUID { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("middleName")]
        public string MiddleName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("enrolledInRewards")]
        public bool EnrolledInRewards { get; set; }

        [JsonProperty("rewardsEnrollmentErrorDescription")]
        public string RewardsEnrollmentErrorDescription { get; set; }

        [JsonProperty("activityId")]
        public string ActivityId { get; set; }

        [JsonProperty("errors")]
        public List<Error> Errors { get; set; } 
    }

    public class ErrorInfo
    {
        [JsonProperty("summary")]
        public string Summary { get; set; }
    }

    public class Error
    {
        [JsonProperty("errorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("errorInfo")]
        public ErrorInfo ErrorInfo { get; set; }

        [JsonProperty("diagnosticId")]
        public int DiagnosticId { get; set; }
    }
}
