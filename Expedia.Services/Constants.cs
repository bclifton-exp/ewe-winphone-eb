using System.Text.RegularExpressions;

namespace Expedia.Client
{
	public static class Constants
	{
		public const string AccessTokenExpiredErrorCode = "401";
		public static readonly string[] AuthenticationCookieNames = new[] {"accttype", "tpid", "user", "minfo", "linfo"};
		
		public static class Urls
		{
			public const string BaseUrlFormat = "https://www.{0}/";
			public const string TrunkUrlFormat = "http://www{0}.trunk.sb.karmalab.net/";
			public const string FallbackDomain = "expedia.com";
			public const string BaseSuggestionUrl = "https://suggest.expedia.com/";
			public const string FacebookGraphUrl = "https://graph.facebook.com/v2.2/";

            public const string AuthenticationApiRoot = "api/auth/";
			public const string FlightsApiRoot = "api/flight/";
			public const string MobileHotelsApiRoot = "m/api/hotel/";
		    public const string MobileCarsApiRoot = "m/api/cars/search/";
            public const string SuggestionsApiRoot = "api/v4/";
            public const string SuggestionsApiRootOlder = "hint/es/v1/";
			public const string TripsApiRoot = "api/trips/";
			public const string UrgencyApiRoot = "api/urgency/";
			public const string UserApiRoot = "api/user/";
		}

		public static class SuggestionServiceApiPath
		{
            public const string TypeAhead = "typeahead/";
            public const string Nearby = "nearby/";
		}

		public static class UrgencyServiceApiPath
		{
			public const string RegionActivity = "regionActivity/";
		}

		public static class LinkingStatus
		{
			public const string FacebookLinkedWithOther = "fbaccountalreadylinkedtoothers";
			public const string FacebookNotFound = "nofbdatafound";
			public const string FacebookNotLinked = "notLinked";
			public const string FacebookExisting = "existing";
			public const string FacebookLinked = "success";
			public const string FacebookError = "error";
		}

	    public static class UrlActions
	    {
	        public const string HotelSearch = "search?";
	    }

        public static class Facebook
        {
            //public const string ConnectUrl = "fbconnect://authorize?client_id={0}&scope=basic_info&redirect_uri={1}";
            public const string ConnectUrl = "https://www.facebook.com/dialog/oauth?client_id={0}&redirect_uri={1}&response_type={2}&scope={3}&display=popup";
            public const string Scope = "user_about_me,email";
            public const string ResponseType = "token";
            public const string ExpediaClientAppId = "131538103586818";
            public const string RedirectUrl = "https://www.facebook.com/connect/login_success.html";
            public const string ResponsePattern = "http.+#access_token=(?<token>.+)&expires_in=(?<expiry>.+)";
            public const string ResponsePatternTokenGroup = "token";
            public const string ResponsePatternExpiryGroup = "expiry";
            public static readonly Regex ResponseRegex = new Regex(ResponsePattern);
        }
    }
}
