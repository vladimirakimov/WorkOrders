using FluentValidation;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Extensions;
using ITG.Brix.WorkOrders.Application.Resources;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class CreateWorkOrderCommandValidator : AbstractValidator<CreateWorkOrderCommand>
    {
        public CreateWorkOrderCommandValidator()
        {
            RuleFor(x => x.Site).NotEmpty().ValidationFault(ValidationFailures.SiteMandatory);
            RuleFor(x => x.UserCreated).NotEmpty().ValidationFault(ValidationFailures.UserCreatedMandatory);
            RuleFor(x => x.Operation).NotEmpty().ValidationFault(ValidationFailures.OperationMandatory);
        }
    }
}
