using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;

namespace Expedia.Services.Interfaces
{
    public interface ISettingsService
    {
        string GetCurrentCountryId();
        void SetGetCurrentCountryId(string countryId);
        string GetCurrentCultureCode();
        void SetCurrentCultureCode(string cultureCode);
        string GetCurrentDomain();
        void SetCurrentDomain(string domain);
        string GetCurrentLocale();
        void SetUserInfo(long uid, string accountName);
        void ClearUserInfo();
        string GetRealName();
        string GetUserToken();
        GeolocationAccessStatus GetUseLocationService();
        void SetUseLocationService(GeolocationAccessStatus isUsingLocationSerivce);
    }
}
