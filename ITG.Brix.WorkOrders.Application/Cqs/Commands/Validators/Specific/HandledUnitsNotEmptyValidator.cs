using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsNotEmptyValidator : PropertyValidator
    {
        public HandledUnitsNotEmptyValidator() : base("HandledUnits cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var handledUnits = (Optional<IEnumerable<HandledUnitDto>>)context.PropertyValue;
            if (handledUnits.HasValue && (handledUnits.Value == null || !handledUnits.Value.Any()))
            {
                return false;
            }

            return true;
        }
    }
}
