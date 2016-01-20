using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Expedia.Services.Query
{
    public class ApiRequest
    {
        private StringBuilder Builder { get; set; }
        private List<KeyValuePair<string,string>> PayloadParams { get; set; } 

        public ApiRequest(string baseURL)
        {
            Builder = new StringBuilder();
            Builder.Append(baseURL);
            PayloadParams = new List<KeyValuePair<string, string>>();
        }

        public void AppendPath(string path)
        {
            Builder.Append(path);
        }

        public void AppendSearchQuery(string query)
        {
            Builder.Append(query + "?");
        }

        public void AppendLocaleParam(string locale)
        {
            Builder.Append("&locale=" + locale.Replace("-", "_"));
        }

        public void AppendParam(string paramName, string value)
        {
            Builder.Append("&" + paramName + "=" + value);
        }

        public void PayloadParam(string paramName, string value)
        {
            PayloadParams.Add(new KeyValuePair<string, string>(paramName,value));
        }

        public Uri GetFullUri()
        {
            return new Uri(Builder.ToString());
        }

        public FormUrlEncodedContent GetPayloadContent()
        {
            return new FormUrlEncodedContent(PayloadParams);
        }
    }
}
