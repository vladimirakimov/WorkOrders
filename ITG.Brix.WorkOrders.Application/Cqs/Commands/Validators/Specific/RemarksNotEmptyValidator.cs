using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class RemarksNotEmptyValidator : PropertyValidator
    {
        public RemarksNotEmptyValidator() : base("Remarks cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var remarks = (Optional<IEnumerable<RemarkDto>>)context.PropertyValue;
            if (remarks.HasValue && (remarks.Value == null || !remarks.Value.Any()))
            {
                return false;
            }

            return true;
        }
    }
}
