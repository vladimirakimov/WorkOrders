using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class InputsEachElemOperantIdInvalidValidator : PropertyValidator
    {
        public InputsEachElemOperantIdInvalidValidator() : base("Inputs[{Index}].OperantId has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var inputs = (Optional<IEnumerable<InputDto>>)context.PropertyValue;
            if (inputs.HasValue && inputs.Value != null && inputs.Value.Any())
            {
                var index = 0;
                foreach (var input in inputs.Value)
                {
                    if (input != null && (!Guid.TryParse(input.OperantId, out Guid operantId) || operantId == default(Guid)))
                    {
                        result = false;
                        context.MessageFormatter.AppendArgument("Key", nameof(input.OperantId));
                        context.MessageFormatter.AppendArgument("Index", index);
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
