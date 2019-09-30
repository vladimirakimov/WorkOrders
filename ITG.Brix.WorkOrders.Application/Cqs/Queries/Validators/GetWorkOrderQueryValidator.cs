using FluentValidation;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Extensions;
using ITG.Brix.WorkOrders.Application.Resources;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Validators
{
    public class GetWorkOrderQueryValidator : AbstractValidator<GetWorkOrderQuery>
    {
        public GetWorkOrderQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.WorkOrderNotFound);
        }
    }
}
