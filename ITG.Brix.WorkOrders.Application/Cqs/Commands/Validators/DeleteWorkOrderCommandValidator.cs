using FluentValidation;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Extensions;
using ITG.Brix.WorkOrders.Application.Resources;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class DeleteWorkOrderCommandValidator : AbstractValidator<DeleteWorkOrderCommand>
    {
        public DeleteWorkOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.WorkOrderNotFound);
        }
    }
}
