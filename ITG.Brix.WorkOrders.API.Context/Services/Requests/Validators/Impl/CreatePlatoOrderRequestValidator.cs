using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Components;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Impl.Bases;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Impl
{
    public class CreatePlatoOrderRequestValidator : AbstractRequestValidator<CreatePlatoOrderRequest>
    {
        private readonly IRequestComponentValidator _requestComponentValidator;

        public CreatePlatoOrderRequestValidator(IRequestComponentValidator requestComponentValidator)
        {
            _requestComponentValidator = requestComponentValidator ?? throw new ArgumentNullException(nameof(requestComponentValidator));
        }

        public override ValidationResult Validate<T>(T request)
        {
            ValidationResult result = new ValidationResult();

            return result;
        }
    }
}
