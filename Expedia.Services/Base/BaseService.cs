using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Expedia.Services.Base
{
    public class BaseService
    {
        public async Task<string> ExecuteGet(Uri requestUri, CancellationToken ct)
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

        public async Task<string> ExecutePost(Uri requestUri, HttpContent content, CancellationToken ct)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    HttpResponseMessage response = await client.PostAsync(requestUri, content, ct);

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
