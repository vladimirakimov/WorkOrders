using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class ListPropertyRequest
    {
        private readonly ListPropertiesFromQuery _query;

        public ListPropertyRequest(ListPropertiesFromQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string Filter => _query.Filter;

        public string Skip => _query.Skip;

        public string Top => _query.Top;
    }
}
