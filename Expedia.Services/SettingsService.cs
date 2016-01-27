using System;
using Windows.Devices.Geolocation;
using Windows.Storage;
using Expedia.Services.Interfaces;

namespace Expedia.Services
{
    public class SettingsService : ISettingsService
    {
        public ApplicationDataContainer LocalSettings { get; private set; }

        public SettingsService()
        {
            LocalSettings = ApplicationData.Current.LocalSettings;
        }

        private static string GetDefaultCountryId()
        {
            var tag = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];
            switch (tag)
            {
                case "de-DE":
                    return "DE";
                case "fr-FR":
                    return "FR";
                case "it-IT":
                    return "IT";
                case "en-US":
                    return "US";
                case "en-GB":
                    return "GB";
                case "es-MX":
                    return "MX";
                case "pt-BR":
                    return "BR";
                default:
                    return "US";

            }
        }

        private static string GetDefaultCultureCode()
        {
            var tag = Windows.System.UserProfile.GlobalizationPreferences.Languages[0];

            switch (tag)
            {
                case "de-DE":
                    return tag;
                case "fr-FR":
                    return tag;
                case "it-IT":
                    return tag;
                case "en-US":
                    return tag;
                case "en-GB":
                    return tag;
                case "es-MX":
                    return tag;
                case "pt-BR":
                    return tag;
                default:
                    return "en-US";
            }
        }

        public string GetCurrentCountryId()
        {
            var result = LocalSettings.Values[Settings.GetCurrentCountryId];

            return result == null ? GetDefaultCountryId() : result.ToString();
        }

        public void SetGetCurrentCountryId(string countryId)
        {
            LocalSettings.Values[Settings.GetCurrentCountryId] = countryId;
        }

        public string GetCurrentCultureCode()
        {
            var result = LocalSettings.Values[Settings.CurrentCultureCode];

            return result == null ? GetDefaultCultureCode() : result.ToString();
        }

        public void SetCurrentCultureCode(string cultureCode)
        {
            LocalSettings.Values[Settings.CurrentCultureCode] = cultureCode;
        }

        public string GetCurrentDomain()
        {
            var result = LocalSettings.Values[Settings.Domain];
            return result == null ? "expedia.com" : result.ToString();
        }

        public void SetCurrentDomain(string domain)
        {
            LocalSettings.Values[Settings.Domain] = domain;
        }

        public string GetCurrentLocale()
        {
            return GetCurrentCultureCode().Replace('-', '_');
        }

        public void SetUserInfo(long uid, string accountName)
        {
            LocalSettings.Values[Settings.UserToken] = uid;
            LocalSettings.Values[Settings.AccountName] = accountName;
        }

        public void ClearUserInfo()
        {
            LocalSettings.Values[Settings.UserToken] = null;
            LocalSettings.Values[Settings.AccountName] = null;
        }

        public string GetRealName()
        {
            var result = LocalSettings.Values[Settings.AccountName];
            return result?.ToString();
        }

        public string GetUserToken()
        {
            var result = LocalSettings.Values[Settings.UserToken];
            return result?.ToString();
        }

        public GeolocationAccessStatus GetUseLocationService()
        {
            GeolocationAccessStatus status;
            var firstTime = LocalSettings.Values[Settings.UseLocationService];
            if(firstTime == null)
                return GeolocationAccessStatus.Unspecified;

            Enum.TryParse(LocalSettings.Values[Settings.UseLocationService].ToString(), out status);
            return status;
        }

        public void SetUseLocationService(GeolocationAccessStatus accessStatus)
        {
            LocalSettings.Values[Settings.UseLocationService] = accessStatus.ToString();
        }

        public T GetSetting<T>(string setting)
        {
            return (T)LocalSettings.Values[setting];
        }
    }
}
