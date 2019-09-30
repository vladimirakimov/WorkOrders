using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class PicturesEachElemNameNotEmptyValidator : PropertyValidator
    {
        public PicturesEachElemNameNotEmptyValidator() : base("Pictures[{Index}].Name cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var pictures = (Optional<IEnumerable<PictureDto>>)context.PropertyValue;
            if (pictures.HasValue && pictures.Value != null && pictures.Value.Any())
            {
                var index = 0;
                foreach (var picture in pictures.Value)
                {
                    if (picture != null && string.IsNullOrWhiteSpace(picture.Name))
                    {
                        result = false;
                        context.MessageFormatter.AppendArgument("Key", nameof(picture.Name));
                        context.MessageFormatter.AppendArgument("Index", index);
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
