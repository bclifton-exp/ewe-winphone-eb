using System;
using System.Collections.Generic;
using System.Text;
using Expedia.Entities;

namespace Expedia.Services.Query
{
    public class QueryBuilder
    {
        private StringBuilder Builder { get; set; }
        private string ApiQuery { get; set; }

        public QueryBuilder(IQueryParameters queryParameters)
        {
            Builder = new StringBuilder();
            BuildApiQuery(queryParameters);
        }

        private void BuildApiQuery(IQueryParameters queryParameters)
        {
            queryParameters.AppendParameters((s, s1) => Builder.Append("&" + s + "=" + s1));

            var query = Builder.ToString();

            ApiQuery = query;
        }

        public string ReturnQueryUri()
        {
            return ApiQuery;
        }
    }
}
