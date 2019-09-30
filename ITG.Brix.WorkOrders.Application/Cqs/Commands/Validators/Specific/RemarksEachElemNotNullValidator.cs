using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class RemarksEachElemNotNullValidator : PropertyValidator
    {
        public RemarksEachElemNotNullValidator() : base("Remarks[{Index}] cannot be null.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var remarks = (Optional<IEnumerable<RemarkDto>>)context.PropertyValue;
            if (remarks.HasValue && remarks.Value != null && remarks.Value.Any())
            {
                var index = 0;
                foreach (var remark in remarks.Value)
                {
                    if (remark == null)
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
