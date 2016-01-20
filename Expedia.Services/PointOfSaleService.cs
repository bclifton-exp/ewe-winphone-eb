using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Expedia.Entities.AppEnvContext;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;

namespace Expedia.Services
{
    public class PointOfSaleService : BaseService, IPointOfSaleService
    {
        private readonly ResourceLoader _resourceLoader;
        private readonly ISettingsService _settingsService;

        private readonly Lazy<PointOfSale[]> _pointsOfSale;

        public PointOfSaleService(ISettingsService settingsService)
        {
            _resourceLoader = new ResourceLoader();
            _settingsService = settingsService;

            _pointsOfSale = new Lazy<PointOfSale[]>(CreatePointsOfSale);
        }

        public Task<PointOfSale[]> GetPointsOfSale()
        {
            return Task.FromResult(_pointsOfSale.Value);
        }

        public Task<PointOfSale> GetCurrentPointOfSale()
        {
            return FindPointOfSale(_settingsService.GetCurrentCountryId());
        }

        public async Task SetCurrentPointOfSale(string countryId)
        {
            _settingsService.SetGetCurrentCountryId(countryId);

            var pos = await FindPointOfSale(countryId);

            if (pos == null)
            {
               //No Default PoS Set
            }
            else
            {
                _settingsService.SetCurrentDomain(pos.HostName);
            }
        }

        public async Task<PointOfSaleCulture[]> GetCultures()
        {
            var country = await GetCurrentPointOfSale();

            return country.Cultures;
        }

        public Task<PointOfSaleCulture> GetCurrentCulture()
        {
            return FindCulture(_settingsService.GetCurrentCultureCode());
        }

        public void SetCurrentCulture(string cultureCode)
        {
            _settingsService.SetCurrentCultureCode(cultureCode);
        }

        private async Task<PointOfSale> FindPointOfSale(string countryId)
        {
            var countries = await GetPointsOfSale();
            return countries.FirstOrDefault(country => country.CountryId.Equals(countryId, StringComparison.OrdinalIgnoreCase));
        }

        private async Task<PointOfSaleCulture> FindCulture(string cultureCode)
        {
            var country = await GetCurrentPointOfSale();

            return country
                .Cultures
                .FirstOrDefault(culture => culture.CultureCode.Equals(cultureCode, StringComparison.OrdinalIgnoreCase));
        }

        private PointOfSale[] CreatePointsOfSale()
        {
            return new[]
            {
                //new PointOfSale
                //{
                //    CountryId = "BR",
                //    DisplayName = _resourceLoader.GetString("CountryName_BR"),
                //    HostName = _resourceLoader.GetString("HostName_BR"),
                //    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_BR"),
                //    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_BR"),
                //    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_BR"),
                //    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_BR"),
                //    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_BR"),
                //    Cultures = new []
                //    {
                //        new PointOfSaleCulture
                //        {
                //            CultureCode = "pt-BR",
                //        }
                //    }
                //},
                //new PointOfSale
                //{
                //    CountryId = "DE",
                //    DisplayName = _resourceLoader.GetString("CountryName_DE"),
                //    HostName = _resourceLoader.GetString("HostName_DE"),
                //    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_DE"),
                //    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_DE"),
                //    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_DE"),
                //    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_DE"),
                //    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_DE"),
                //    Cultures = new []
                //    {
                //        new PointOfSaleCulture
                //        {
                //            CultureCode = "de-DE",
                //        }
                //    }
                //},
                //new PointOfSale
                //{
                //    CountryId = "FR",
                //    DisplayName = _resourceLoader.GetString("CountryName_FR"),
                //    HostName = _resourceLoader.GetString("HostName_FR"),
                //    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_FR"),
                //    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_FR"),
                //    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_FR"),
                //    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_FR"),
                //    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_FR"),
                //    Cultures = new []
                //    {
                //        new PointOfSaleCulture
                //        {
                //            CultureCode = "fr-FR",
                //        }
                //    }
                //},
                //new PointOfSale
                //{
                //    CountryId = "IT",
                //    DisplayName = _resourceLoader.GetString("CountryName_IT"),
                //    HostName = _resourceLoader.GetString("HostName_IT"),
                //    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_IT"),
                //    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_IT"),
                //    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_IT"),
                //    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_IT"),
                //    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_IT"),
                //    Cultures = new []
                //    {
                //        new PointOfSaleCulture
                //        {
                //            CultureCode = "it-IT",
                //        }
                //    }
                //},
                //new PointOfSale
                //{
                //    CountryId = "MX",
                //    DisplayName = _resourceLoader.GetString("CountryName_MX"),
                //    HostName = _resourceLoader.GetString("HostName_MX"),
                //    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_MX"),
                //    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_MX"),
                //    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_MX"),
                //    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_MX"),
                //    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_MX"),
                //    Cultures = new []
                //    {
                //        new PointOfSaleCulture
                //        {
                //            CultureCode = "es-MX",
                //        }
                //    }
                //},
                //new PointOfSale
                //{
                //    CountryId = "GB",
                //    DisplayName = _resourceLoader.GetString("CountryName_GB"),
                //    HostName = _resourceLoader.GetString("HostName_GB"),
                //    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_GB"),
                //    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_GB"),
                //    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_GB"),
                //    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_GB"),
                //    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_GB"),
                //    Cultures = new[]
                //    {
                //        new PointOfSaleCulture
                //        {
                //            CultureCode = "en-GB",
                //        }
                //    }
                //},
                new PointOfSale
                {
                    CountryId = "US",
                    DisplayName = _resourceLoader.GetString("CountryName_US"),
                    HostName = _resourceLoader.GetString("HostName_US"),
                    HelpUri = _resourceLoader.GetString("PosHelpPageUrl_US"),
                    AboutUri = _resourceLoader.GetString("PosAboutPageUrl_US"),
                    PrivacyUri = _resourceLoader.GetString("PosPrivacyPageUrl_US"),
                    TermsOfUseUri = _resourceLoader.GetString("PosTermsOfUsePageUrl_US"),
                    DateFormatHotel = _resourceLoader.GetString(" PosDateFormatHotel_US"),
                    Cultures = new []
                    {
                        new PointOfSaleCulture
                        {
                            CultureCode = "en-US",
                        }
                    }
                }

            };
        }
    }
}
