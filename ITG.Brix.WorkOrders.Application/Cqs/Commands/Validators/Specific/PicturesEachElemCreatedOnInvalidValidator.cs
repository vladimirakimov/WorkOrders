using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class PicturesEachElemCreatedOnInvalidValidator : PropertyValidator
    {
        public PicturesEachElemCreatedOnInvalidValidator() : base("Pictures[{Index}].CreatedOn has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var pictures = (Optional<IEnumerable<PictureDto>>)context.PropertyValue;
            if (pictures.HasValue && pictures.Value != null && pictures.Value.Any())
            {
                var index = 0;
                foreach (var picture in pictures.Value)
                {
                    if (picture != null)
                    {
                        var dateTimeProvider = new DateTimeProvider();
                        var resultDateTime = dateTimeProvider.Parse(picture.CreatedOn);
                        if (resultDateTime.HasValue)
                        {
                            var validIso = dateTimeProvider.CheckFormat(picture.CreatedOn);
                            if (validIso)
                            {
                                try
                                {
                                    new CreatedOn(resultDateTime.Value);
                                }
                                catch
                                {
                                    result = false;
                                    context.MessageFormatter.AppendArgument("Key", nameof(picture.CreatedOn));
                                    context.MessageFormatter.AppendArgument("Index", index);
                                }
                            }
                            else
                            {
                                result = false;
                                context.MessageFormatter.AppendArgument("Index", index);
                                context.MessageFormatter.AppendArgument("Key", nameof(picture.CreatedOn));
                            }
                        }
                        else
                        {
                            result = false;
                            context.MessageFormatter.AppendArgument("Key", nameof(picture.CreatedOn));
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
