using System;
using System.Text;

namespace Expedia.Services.Query
{
    public class ApiRequest
    {
        private StringBuilder Builder { get; set; }

        public ApiRequest(string baseURL)
        {
            Builder = new StringBuilder();
            Builder.Append(baseURL);
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

        public Uri Get()
        {
            return new Uri(Builder.ToString());
        }
    }
}
