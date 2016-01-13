using System;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Client;
using Expedia.Entites;
using Expedia.Entities.Suggestions;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;
using Expedia.Services.Query;
using Newtonsoft.Json;

namespace Expedia.Services
{
    public class SuggestionService : BaseService, ISuggestionService
    {
        private ISettingsService _settingsService;

        public SuggestionService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<SuggestionsResponse> Suggest(CancellationToken ct, string query, SuggestionLob lob)
        {
            string regionTypes = "";

            if (lob == SuggestionLob.HOTELS)
            {
                regionTypes = "airport|city|multicity|neighborhood|metrocode|hotel";
            }

            var request = new ApiRequest(Constants.Urls.BaseSuggestionUrl);
            request.AppendPath(Constants.Urls.SuggestionsApiRoot);
            request.AppendPath(Constants.SuggestionServiceApiPath.TypeAhead);
            request.AppendSearchQuery(query);
            request.AppendLocaleParam(_settingsService.GetCurrentLocale());
            request.AppendParam("lob",lob.ToString());
            request.AppendParam("regiontype",regionTypes);
            request.AppendParam("client","App.Windows.Native");//TODO probably will change   

            var result = await Execute(request.Get(), ct);

            return result != null ? JsonConvert.DeserializeObject<SuggestionsResponse>(result) : null;
        }

        public async Task<SuggestionsResponse> Suggest(CancellationToken ct, double latitude, double longitude, SuggestionLob lob)
        {
            string regionTypes = "";

            if (lob == SuggestionLob.HOTELS)
            {
                regionTypes = "airport|city|hotel|poi";
            }

            if (lob == SuggestionLob.FLIGHTS)
            {
                regionTypes = "airport";
            }

            var request = new ApiRequest(Constants.Urls.BaseSuggestionUrl);
            request.AppendPath(Constants.Urls.SuggestionsApiRoot);
            request.AppendPath(Constants.SuggestionServiceApiPath.Nearby + "?");
            request.AppendLocaleParam(_settingsService.GetCurrentLocale());
            request.AppendParam("latlong", "{0:#.#####}|{1:#.#####}".InvariantCultureFormat(latitude, longitude));
            request.AppendParam("regiontype", regionTypes);
            request.AppendParam("sort", "d"); // Distance
            request.AppendParam("lob", lob.ToString());
            request.AppendParam("client", "App.Windows.Native");//TODO probably will change   

            var result = await Execute(request.Get(), ct);

            return result != null ? JsonConvert.DeserializeObject<SuggestionsResponse>(result) : null;
        }
    }
}
