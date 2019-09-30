using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class ListWorkOrderRequest
    {
        private readonly ListWorkOrderFromQuery _query;

        public ListWorkOrderRequest(ListWorkOrderFromQuery query)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string Filter => _query.Filter;

        public string Top => _query.Top;

        public string Skip => _query.Skip;

        public void Unescape()
        {
            if (!string.IsNullOrWhiteSpace(_query.Filter))
            {
                _query.Filter = Uri.UnescapeDataString(_query.Filter);
            }
        }
    }
}
