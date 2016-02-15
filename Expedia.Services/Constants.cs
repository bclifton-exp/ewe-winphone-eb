using System;
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

        public static TimeSpan TimerUpdateTiles()
        {
            return TimeSpan.FromMinutes(5);
        }

        public static readonly Regex MatchExpediaHostname = new Regex("^https?://((?:(?:www\\.)(?:expedia.[\\w|\\.]*))|(?:wwwexpediacom.[\\w|\\.]*))/?(?:.*)$", RegexOptions.IgnoreCase);
        public static readonly Regex MatchFeedbackHostname = new Regex(@"^https?://(?:\w+(?!expedia)\.?)*((?:www)?opinionlab(?:\w*)\.[\w|\.]+)/?(?:.*)$", RegexOptions.IgnoreCase);

        #region All Known File Extensions and URI Schemes

        /// <summary>
        /// If it's one of these, we'll need to launch the media player
        /// from http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj207065(v=vs.105).aspx
        /// </summary>
        public static readonly string[] PlayMediaFileExtensions =
        {
            ".3gp",
            ".3g2",
            ".3gpp",
            ".3gpp2",
            ".aac",
            ".aetx",
            ".asf",
            ".avi",
            ".m1v",
            ".m2v",
            ".m4a",
            ".m4r",
            ".m4v",
            ".mkv",
            ".mov",
            ".mp3",
            ".mp4",
            ".mpe",
            ".mpeg",
            ".mpg",
            ".qcp",
            ".wav",
            ".wdp",
            ".wma",
            ".wmv"
        };

        /// <summary>
        /// If it's one of these, we'll need to launch the appropriate handler
        /// from http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj207065(v=vs.105).aspx
        /// </summary>
        public static readonly string[] LaunchFileExtensions =
        {
            ".rtf",
            ".tif",
            ".tiff",
            ".one",
            ".onetoc2",
            ".doc",
            ".docm",
            ".docx",
            ".dot",
            ".dotm",
            ".dotx",
            ".pdf",
            ".pptx",
            ".pptm",
            ".potx",
            ".potm",
            ".ppam",
            ".ppsx",
            ".ppsm",
            ".ppt",
            ".pps",
            ".xls",
            ".xlm",
            ".xlt",
            ".xlsx",
            ".xlsm",
            ".xltx",
            ".xltm",
            ".xlsb",
            ".xlam",
            ".xll",
            ".cer",
            ".hdp",
            ".ico",
            ".icon",
            ".jxr",
            ".p7b",
            ".pem",
            ".txt",
            ".url",
            ".vcf",
            ".xap",
            ".xht",
            ".xsl",
            ".zip"
        };

        /// <summary>
        /// IE will handle these protocols for us
        /// </summary>
        public static readonly string[] LaunchUriSchemes =
        {
            "callto",
            "mailto",
            "ms-",
            "onenote",
            "wallet",
            "zune"
        };


        /// <summary>
        /// We can skip a bunch of logic if we see these common extensions
        /// </summary>
        public static readonly string[] BypassList =
        {
            ".com",
            ".htm",
            ".html",
            ".asp",
            ".aspx",
            ".js",
            ".xml"
        };

        #endregion

        public const string HotelImagesUrl = "https://media1.expedia.com";

        public const int MaximumNumberOfAdults = 6;
        public const int MaximumNumberOfChildren = 5;
        public const int MaximumNumberOfPerson = 6;
        public const int MaximumChildAge = 17;

        public const string AkamaiHeaderKey = "x-akamai-device-characteristics";
        public const string AkamaiHeaderValue = "is_tablet=true;is_wireless_device=true;is_mobile=true";
    }
}
