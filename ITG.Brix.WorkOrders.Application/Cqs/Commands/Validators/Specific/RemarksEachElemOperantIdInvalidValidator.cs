using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class RemarksEachElemOperantIdInvalidValidator : PropertyValidator
    {
        public RemarksEachElemOperantIdInvalidValidator() : base("Remarks[{Index}].OperantId has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var remarks = (Optional<IEnumerable<RemarkDto>>)context.PropertyValue;
            if (remarks.HasValue && remarks.Value != null && remarks.Value.Any())
            {
                var index = 0;
                foreach (var remark in remarks.Value)
                {
                    if (remark != null && (!Guid.TryParse(remark.OperantId, out Guid operantId) || operantId == default(Guid)))
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
