using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class PicturesNotEmptyValidator : PropertyValidator
    {
        public PicturesNotEmptyValidator() : base("Pictures cannot be null or empty.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var pictures = (Optional<IEnumerable<PictureDto>>)context.PropertyValue;
            if (pictures.HasValue && (pictures.Value == null || !pictures.Value.Any()))
            {
                return false;
            }

            return true;
        }
    }
}
