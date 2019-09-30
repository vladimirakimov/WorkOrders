using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class RemarksEachElemOperantIdNotEmptyValidator : PropertyValidator
    {
        public RemarksEachElemOperantIdNotEmptyValidator() : base("Remarks[{Index}].OperantId cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var remarks = (Optional<IEnumerable<RemarkDto>>)context.PropertyValue;
            if (remarks.HasValue && remarks.Value != null && remarks.Value.Any())
            {
                var index = 0;
                foreach (var remark in remarks.Value)
                {
                    if (remark != null && string.IsNullOrWhiteSpace(remark.OperantId))
                    {
                        result = false;
                        context.MessageFormatter.AppendArgument("Key", nameof(remark.OperantId));
                        context.MessageFormatter.AppendArgument("Index", index);
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
