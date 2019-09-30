using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsEachElemNotNullValidator : PropertyValidator
    {
        public HandledUnitsEachElemNotNullValidator() : base("HandledUnits[{Index}] cannot be null.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var handledUnits = (Optional<IEnumerable<HandledUnitDto>>)context.PropertyValue;
            if (handledUnits.HasValue && handledUnits.Value != null && handledUnits.Value.Any())
            {
                var index = 0;
                foreach (var handledUnit in handledUnits.Value)
                {
                    if (handledUnit == null)
                    {
                        result = false;
                        context.MessageFormatter.AppendArgument("Index", index);
                    }
                    index++;
                }
            }

            return result;
        }
    }
}
