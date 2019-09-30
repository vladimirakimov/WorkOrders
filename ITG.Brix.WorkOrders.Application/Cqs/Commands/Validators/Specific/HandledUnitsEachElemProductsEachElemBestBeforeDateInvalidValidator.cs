using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsEachElemProductsEachElemBestBeforeDateInvalidValidator : PropertyValidator
    {
        public HandledUnitsEachElemProductsEachElemBestBeforeDateInvalidValidator() : base("HandledUnits[{Index}][{IndexProduct}].BestBeforeDate has invalid value.") { }

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
                            if (product != null)
                            {
                                var dateTimeProvider = new DateTimeProvider();

                                var resultDateTime = dateTimeProvider.Parse(product.BestBeforeDate);

                                if (resultDateTime.HasValue)
                                {
                                    var validIso = dateTimeProvider.CheckFormat(product.BestBeforeDate);
                                    if (validIso)
                                    {
                                        try
                                        {
                                            new DateOn(resultDateTime.Value);
                                        }
                                        catch
                                        {
                                            result = false;
                                            context.MessageFormatter.AppendArgument("Index", index);
                                            context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                            context.MessageFormatter.AppendArgument("Key", nameof(product.BestBeforeDate));
                                        }
                                    }
                                    else
                                    {
                                        result = false;
                                        context.MessageFormatter.AppendArgument("Index", index);
                                        context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                        context.MessageFormatter.AppendArgument("Key", nameof(product.BestBeforeDate));
                                    }
                                }
                                else
                                {
                                    if (product.BestBeforeDate != null)
                                    {
                                        result = false;
                                        context.MessageFormatter.AppendArgument("Index", index);
                                        context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                        context.MessageFormatter.AppendArgument("Key", nameof(product.BestBeforeDate));
                                    }
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
