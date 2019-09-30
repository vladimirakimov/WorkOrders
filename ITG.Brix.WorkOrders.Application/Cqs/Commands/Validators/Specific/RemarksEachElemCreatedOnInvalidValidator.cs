using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class RemarksEachElemCreatedOnInvalidValidator : PropertyValidator
    {
        public RemarksEachElemCreatedOnInvalidValidator() : base("Remarks[{Index}].CreatedOn has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var remarks = (Optional<IEnumerable<RemarkDto>>)context.PropertyValue;
            if (remarks.HasValue && remarks.Value != null && remarks.Value.Any())
            {
                var index = 0;
                foreach (var remark in remarks.Value)
                {
                    if (remark != null)
                    {
                        var dateTimeProvider = new DateTimeProvider();
                        var resultDateTime = dateTimeProvider.Parse(remark.CreatedOn);
                        if (resultDateTime.HasValue)
                        {
                            var validIso = dateTimeProvider.CheckFormat(remark.CreatedOn);
                            if (validIso)
                            {
                                try
                                {
                                    new CreatedOn(resultDateTime.Value);
                                }
                                catch
                                {
                                    result = false;
                                    context.MessageFormatter.AppendArgument("Index", index);
                                    context.MessageFormatter.AppendArgument("Key", nameof(remark.CreatedOn));

                                }
                            }
                            else
                            {
                                result = false;
                                context.MessageFormatter.AppendArgument("Index", index);
                                context.MessageFormatter.AppendArgument("Key", nameof(remark.CreatedOn));
                            }
                        }
                        else
                        {
                            result = false;
                            context.MessageFormatter.AppendArgument("Index", index);
                            context.MessageFormatter.AppendArgument("Key", nameof(remark.CreatedOn));

                        }
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
