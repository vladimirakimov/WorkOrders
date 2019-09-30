using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Components;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Impl
{
    public class GetWorkOrderRequestValidator : AbstractRequestValidator<GetWorkOrderRequest>
    {

        private readonly IRequestComponentValidator _requestComponentValidator;

        public GetWorkOrderRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }

        public override ValidationResult Validate<T>(T request)
        {
            var req = request as GetWorkOrderRequest;

            ValidationResult result;

            result = _requestComponentValidator.RouteId(req.RouteId);

            if (result == null)
            {
                result = _requestComponentValidator.QueryApiVersionRequired(req.QueryApiVersion);
            }

            if (result == null)
            {
                result = _requestComponentValidator.QueryApiVersion(req.QueryApiVersion);
            }

            if (result == null)
            {
                result = new ValidationResult();
            }

            return result;
        }
    }
}
