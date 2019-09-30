using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class GetWorkOrderRequest
    {
        private readonly GetWorkOrderFromRoute _route;
        private readonly GetWorkOrderFromQuery _query;

        public GetWorkOrderRequest(GetWorkOrderFromRoute route, GetWorkOrderFromQuery query)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;
    }
}
