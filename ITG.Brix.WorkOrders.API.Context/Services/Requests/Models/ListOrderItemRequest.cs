using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class ListOrderItemRequest
    {
        private readonly ListOrderItemFromQuery _query;

        public ListOrderItemRequest(ListOrderItemFromQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string Filter => _query.Filter;

        public string Skip => _query.Skip;

        public string Top => _query.Top;
    }
}
