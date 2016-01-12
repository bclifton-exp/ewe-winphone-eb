using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Expedia.Entities
{
    public static class Constants
    {
        public static readonly UTF8Encoding DefaultEncoding = new UTF8Encoding();
        public const string HttpUserAgent                   = "ExpediaHaF/2.0 (compatible; Windows Phone 8.0)";

        public const string UserSettingsLastNavigationUri  = "deactivatedUrl";

        #region Known URIs and URI Helpers support

		//public const string RelativeUriHotel				= "/MobileHotel";
		//public const string RelativeUriHotelNoRedir			= "/MobileHotel?forceNoRedir=1";
		//public const string RelativeUriHotelSearch          = "/MobileHotel/Search";

		//public const string RelativeUriFlights               = "/m/flights";
		//public const string RelativeUriPackages              = "/m/packages";
        
		//// SSL
		//public const string RelativeUriSignIn                = "/m/login";
		//public const string RelativeUriTrips                 = "/m/trips";
		//public const string RelativeUriTrip                  = "/m/trips/{0}";
        
		//public const string RelativeUriApiHotel				= "/m/api/hotel/";
		//public const string RelativeUriApiFlight			= "/api/flight/";

		//// API
		//public const string RelativeUriApiUser				= "/api/user/";
		//public const string RelativeUriApiTrips             = "/api/trips";
		//public const string RelativeUriApiUserHistory       = "/api/userhistory?limit={0}&_={1}"; // limit = 5, _ = tuid

		//public const string RelativeUriApiSuggestions			= "/hint/es/v1/";
		//public const string RelativeUriApiUrgency			= "/api/urgency/";

        public const string FeedbackHostname                = "opinionlab";
        public const string AppBaseUriSettingName           = "AppBaseURI";

        #endregion

        #region Cookie Management

        public static readonly string[] ExcludedCookieNames         = { "__utma", "__utmb", "__utmz", "JSESSION", "__utmc", "iEAPID" };
        public static readonly string[] AuthenticationCookieNames   = { "accttype", "tpid", "user", "minfo", "linfo" };

	    #endregion

        public static string ExpediaPath { get; set; }
        public static string OAuthPath { get; set; }
        public static string AccessTokenExpiredErrorCode { get; set; }

	    // Webwrapper redirection module
		public const string RedirectActionName = "REDIRECT";
		public const string PopOutActionName = "POPOUT";
		public const string NotPopOutActionName = "NOTPOPOUT";
		public const string PopUpActionName = "POPUP";
    }
}
