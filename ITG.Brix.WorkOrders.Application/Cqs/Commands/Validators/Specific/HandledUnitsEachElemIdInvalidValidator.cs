using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsEachElemIdInvalidValidator : PropertyValidator
    {
        public HandledUnitsEachElemIdInvalidValidator() : base("HandledUnits[{Index}].Id has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var handledUnits = (Optional<IEnumerable<HandledUnitDto>>)context.PropertyValue;
            if (handledUnits.HasValue && handledUnits.Value != null && handledUnits.Value.Any())
            {
                var index = 0;
                foreach (var handledUnit in handledUnits.Value)
                {
                    if (handledUnit != null && (!Guid.TryParse(handledUnit.Id, out Guid id) || id == default(Guid)))
                    {
                        result = false;
                        context.MessageFormatter.AppendArgument("Key", nameof(handledUnit.Id));
                        context.MessageFormatter.AppendArgument("Index", index);
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
