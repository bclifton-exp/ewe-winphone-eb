using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reactive.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Expedia.Client;
using Expedia.Entities;
using Expedia.Entities.Hotels;
using Expedia.Services.Base;
using Expedia.Services.Interfaces;
using Expedia.Services.Query;
using Newtonsoft.Json;

namespace Expedia.Services
{
    public class HotelService : BaseService, IHotelService
    {
        private ISettingsService _settingsService;

        public HotelService(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        public async Task<HotelSearchResponse> GetHotels(HotelSearchQueryParameters parameters, CancellationToken ct)
        {
            var urlBase = string.Format(Constants.Urls.BaseUrlFormat, _settingsService.GetCurrentDomain());
            var query = new QueryBuilder(parameters).ReturnQueryUri();

            var request = new ApiRequest(urlBase);
            request.AppendPath(Constants.Urls.MobileHotelsApiRoot);
            request.AppendPath(Constants.UrlActions.HotelSearch);
            request.AppendPath(query);
            var result = await Execute(request.Get(),ct);

            return result != null ? JsonConvert.DeserializeObject<HotelSearchResponse>(result) : null;
        }
    }
}
