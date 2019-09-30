using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class InputsNotEmptyValidator : PropertyValidator
    {
        public InputsNotEmptyValidator() : base("Inputs cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var inputs = (Optional<IEnumerable<InputDto>>)context.PropertyValue;
            if (inputs.HasValue && (inputs.Value == null || !inputs.Value.Any()))
            {
                return false;
            }

            return true;
        }
    }
}
