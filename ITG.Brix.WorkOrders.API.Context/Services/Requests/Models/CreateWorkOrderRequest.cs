using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Models
{
    public class CreateWorkOrderRequest
    {
        private readonly CreateWorkOrderFromQuery _query;
        private readonly CreateWorkOrderFromBody _body;
        public CreateWorkOrderRequest(CreateWorkOrderFromQuery query, CreateWorkOrderFromBody body)
        {
            _query = query ?? throw new ArgumentNullException(nameof(query));
            _body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public string QueryApiVersion => _query.ApiVersion;

        public string BodyUserCreated => _body.UserCreated;
        public string BodyOperation => _body.Operation;
        public string BodySite => _body.Site;
        public string BodyDepartment => _body.Department;
    }
}
