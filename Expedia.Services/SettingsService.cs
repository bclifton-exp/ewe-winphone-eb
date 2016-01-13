using System;
using System.Collections.Generic;
using System.Text;
using Windows.Storage;
using Expedia.Services.Interfaces;
using Expedia.Services;

namespace Expedia.Services
{
    public class SettingsService : ISettingsService
    {
        public ApplicationDataContainer LocalSettings { get; private set; }

        public SettingsService()
        {
            LocalSettings = ApplicationData.Current.LocalSettings;

            //temp, this works
            var pos = LocalSettings.Values[Settings.PointOfSale];
            if(pos == null)
            {
                LocalSettings.Values[Settings.PointOfSale] = "en-US";
            }
        }

        public string GetCurrentDomain()
        {
            //todo
            return "expedia.com";
        }

        public string GetCurrentLocale()
        {
            return LocalSettings.Values[Settings.PointOfSale].ToString();
        }
    }
}
