using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class InputsEachElemCreatedOnInvalidValidator : PropertyValidator
    {
        public InputsEachElemCreatedOnInvalidValidator() : base("Inputs[{Index}].CreatedOn has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var inputs = (Optional<IEnumerable<InputDto>>)context.PropertyValue;
            if (inputs.HasValue && inputs.Value != null && inputs.Value.Any())
            {
                var index = 0;
                foreach (var input in inputs.Value)
                {
                    if (input != null)
                    {
                        var dateTimeProvider = new DateTimeProvider();
                        var resultDateTime = dateTimeProvider.Parse(input.CreatedOn);
                        if (resultDateTime.HasValue)
                        {
                            try
                            {
                                new CreatedOn(resultDateTime.Value);
                            }
                            catch
                            {
                                result = false;
                                context.MessageFormatter.AppendArgument("Key", nameof(input.CreatedOn));
                                context.MessageFormatter.AppendArgument("Index", index);
                            }
                        }
                        else
                        {
                            result = false;
                            context.MessageFormatter.AppendArgument("Key", nameof(input.CreatedOn));
                            context.MessageFormatter.AppendArgument("Index", index);
                        }
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
