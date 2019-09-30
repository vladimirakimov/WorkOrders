using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsEachElemProductsEachElemQuantityInvalidValidator : PropertyValidator
    {
        public HandledUnitsEachElemProductsEachElemQuantityInvalidValidator() : base("HandledUnits[{Index}][{IndexProduct}].Quantity has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var handledUnits = (Optional<IEnumerable<HandledUnitDto>>)context.PropertyValue;
            if (handledUnits.HasValue && handledUnits.Value != null && handledUnits.Value.Any())
            {
                var index = 0;
                foreach (var handledUnit in handledUnits.Value)
                {
                    if (handledUnit != null && handledUnit.Products != null && handledUnit.Products.Any())
                    {
                        var indexProduct = 0;
                        foreach (var product in handledUnit.Products)
                        {
                            if (product != null && !string.IsNullOrWhiteSpace(product.Quantity))
                            {

                                var resultConvertion = int.TryParse(product.Quantity, out int quantity);
                                if (resultConvertion)
                                {
                                    try
                                    {
                                        new Quantity(quantity);
                                    }
                                    catch
                                    {
                                        result = false;
                                        context.MessageFormatter.AppendArgument("Index", index);
                                        context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                        context.MessageFormatter.AppendArgument("Key", nameof(product.Quantity));
                                    }
                                }
                                else
                                {
                                    result = false;
                                    context.MessageFormatter.AppendArgument("Index", index);
                                    context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                    context.MessageFormatter.AppendArgument("Key", nameof(product.Quantity));
                                }
                            }

                            indexProduct++;
                        }
                    }

                    index++;
                }
            }

            return result;
        }
    }
}
