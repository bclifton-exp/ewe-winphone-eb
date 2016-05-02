using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Windows.Graphics.Printing.OptionDetails;
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
                regionTypes = "airport|city|multicity|neighborhood|metrocode|hotel|poi";
            }

            if (lob == SuggestionLob.CARS)
            {
                regionTypes = "airport|city|multicity|neighborhood|metrocode|poi";
            }

            var request = new ApiRequest(Constants.Urls.BaseSuggestionUrl);
            request.AppendPath(Constants.Urls.SuggestionsApiRoot);
            request.AppendPath(Constants.SuggestionServiceApiPath.TypeAhead);
            request.AppendSearchQuery(query);
            request.AppendLocaleParam(_settingsService.GetCurrentLocale());
            request.AppendParam("lob",lob.ToString());
            request.AppendParam("regiontype",regionTypes);
            request.AppendParam("client", "App.WindowsUWP.Native");

            var result = await ExecuteGet(request.GetFullUri(), ct);

            var deserializedResults = result != null ? JsonConvert.DeserializeObject<SuggestionsResponse>(result) : null;

            if (deserializedResults == null || deserializedResults.Suggestions == null) return null;
            deserializedResults.SortedSuggestionsList = SortByMulticity(deserializedResults);

            return deserializedResults;
        }

        public async Task<SuggestionsResponse> Suggest(CancellationToken ct, double latitude, double longitude, SuggestionLob lob)
        {
            string regionTypes = "";

            if (lob == SuggestionLob.HOTELS)
            {
                regionTypes = "airport|city|hotel";
            }

            if (lob == SuggestionLob.FLIGHTS)
            {
                regionTypes = "airport";
            }

            if (lob == SuggestionLob.CARS)
            {
                regionTypes = "airport|city|multicity|metrocode";
            }

            var request = new ApiRequest(Constants.Urls.BaseSuggestionUrl);
            request.AppendPath(Constants.Urls.SuggestionsApiRoot);
            request.AppendPath(Constants.SuggestionServiceApiPath.Nearby + "?");
            request.AppendLocaleParam(_settingsService.GetCurrentLocale());
            request.AppendParam("latlong", "{0:#.#####}|{1:#.#####}".InvariantCultureFormat(latitude, longitude));
            request.AppendParam("regiontype", regionTypes);
            request.AppendParam("sort", "d"); // Distance
            request.AppendParam("lob", lob.ToString());
            request.AppendParam("client", "App.WindowsUWP.Native"); 

            var result = await ExecuteGet(request.GetFullUri(), ct);

            var deserializedResults = result != null ? JsonConvert.DeserializeObject<SuggestionsResponse>(result) : null;

            if (deserializedResults == null) return null;
            deserializedResults.SortedSuggestionsList = SortByMulticity(deserializedResults);

            return deserializedResults;
        }

        public async Task<SuggestionCoordinates> GetAirportCoordinates(CancellationToken ct, string airportCode, SuggestionLob lob = SuggestionLob.FLIGHTS)
        {
            string regionTypes = "";

            var request = new ApiRequest(Constants.Urls.BaseSuggestionUrl);
            request.AppendPath(Constants.Urls.SuggestionsApiRoot);
            request.AppendPath(Constants.SuggestionServiceApiPath.TypeAhead);
            request.AppendSearchQuery(airportCode);
            request.AppendLocaleParam(_settingsService.GetCurrentLocale());
            request.AppendParam("lob", lob.ToString());
            request.AppendParam("regiontype", regionTypes);
            request.AppendParam("client", "App.WindowsUWP.Native");  

            var result = await ExecuteGet(request.GetFullUri(), ct);

            var deserializedResults = result != null ? JsonConvert.DeserializeObject<SuggestionsResponse>(result) : null;

            if (deserializedResults == null || deserializedResults.Suggestions == null) return null;

            return deserializedResults.Suggestions.First().Coordinates;
        }

        private List<List<SuggestionResult>> SortByMulticity(SuggestionsResponse deserializedResults)
        {
            var multiCityResults = deserializedResults.Suggestions.Where(suggestion => suggestion.Type == SuggestionType.Multicity).ToList();
            var neighborhoodResults = deserializedResults.Suggestions.Where(suggestion => suggestion.Type == SuggestionType.Neighborhood).ToList();
            var unlinkedNeighborhoodResults = deserializedResults.Suggestions.Where(suggestion => suggestion.Type == SuggestionType.Neighborhood).ToList();

            var sortedSuggestions = new List<List<SuggestionResult>>();
            

            foreach (var multiCity in multiCityResults)
            {
                var multiCityNodes = new List<SuggestionResult> {multiCity};
                var linkedNeighborhoods = neighborhoodResults.Where(neighborhood => multiCity.Id == neighborhood.HierarchyInfo.Airport.MultiCity).ToList();
                foreach (var hood in linkedNeighborhoods)
                {
                    hood.IsLinkedToCity = true;
                }
                multiCityNodes.AddRange(linkedNeighborhoods);
                sortedSuggestions.Add(multiCityNodes);
            }
            
            foreach (var neighborhood in from list in sortedSuggestions
                                         from neighborhood in neighborhoodResults
                                         where list.Contains(neighborhood)
                                         select neighborhood)
            {
                unlinkedNeighborhoodResults.Remove(neighborhood);
            }

            sortedSuggestions.Add(unlinkedNeighborhoodResults);

            var otherResults = deserializedResults.Suggestions.Where(suggestion => suggestion.Type != SuggestionType.Multicity && suggestion.Type != SuggestionType.Neighborhood).ToList();
            sortedSuggestions.Add(otherResults);

            return sortedSuggestions;

        }
    }
}
