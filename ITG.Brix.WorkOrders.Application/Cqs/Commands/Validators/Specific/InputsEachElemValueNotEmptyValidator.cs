using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class InputsEachElemValueNotEmptyValidator : PropertyValidator
    {
        public InputsEachElemValueNotEmptyValidator() : base("Inputs[{Index}].Value cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var inputs = (Optional<IEnumerable<InputDto>>)context.PropertyValue;
            if (inputs.HasValue && inputs.Value != null && inputs.Value.Any())
            {
                var index = 0;
                foreach (var input in inputs.Value)
                {
                    if (input != null && string.IsNullOrWhiteSpace(input.Value))
                    {
                        result = false;
                        context.MessageFormatter.AppendArgument("Key", nameof(input.Value));
                        context.MessageFormatter.AppendArgument("Index", index);
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
