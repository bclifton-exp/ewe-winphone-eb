using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Data.Json;
using Expedia.Entities.Hotels;
using Newtonsoft.Json;

namespace Expedia.Services.Base
{
    public class BaseService
    {
        public async Task<string> Execute(Uri requestUri, CancellationToken ct)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.GetAsync(requestUri, ct);

                    var jsonData = await response.Content.ReadAsStringAsync();

                    return jsonData;
                }
                catch (OperationCanceledException)
                {
                    return null; //TODO handle the cancellations
                }
            }
        }
    }
}
