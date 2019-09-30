using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class UpdateWorkOrderRequest
    {
        private readonly UpdateWorkOrderFromRoute _route;
        private readonly UpdateWorkOrderFromQuery _query;
        private readonly UpdateWorkOrderFromHeader _header;
        private readonly UpdateWorkOrderFromBody _body;

        public UpdateWorkOrderRequest(UpdateWorkOrderFromRoute route, UpdateWorkOrderFromQuery query, UpdateWorkOrderFromHeader header, UpdateWorkOrderFromBody body)
        {
            _route = route ?? throw new ArgumentNullException(nameof(route));
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _header = header ?? throw new ArgumentNullException(nameof(header));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string RouteId => _route.Id;

        public string QueryApiVersion => _query.ApiVersion;

        public string HeaderIfMatch => _header.IfMatch;

        public string HeaderContentType => _header.ContentType;

        public string BodyPatch => _body.Patch;
    }
}
