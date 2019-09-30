using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsEachElemProductsEachElemDateFifoInvalidValidator : PropertyValidator
    {
        public HandledUnitsEachElemProductsEachElemDateFifoInvalidValidator() : base("HandledUnits[{Index}][{IndexProduct}].DateFifo has invalid value.") { }

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
                                var resultDateTime = dateTimeProvider.Parse(product.DateFifo);

                                if (resultDateTime.HasValue)
                                {
                                    var validIso = dateTimeProvider.CheckFormat(product.DateFifo);
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
                                            context.MessageFormatter.AppendArgument("Key", nameof(product.DateFifo));
                                        }
                                    }
                                    else
                                    {
                                        result = false;
                                        context.MessageFormatter.AppendArgument("Index", index);
                                        context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                        context.MessageFormatter.AppendArgument("Key", nameof(product.DateFifo));
                                    }
                                }
                                else
                                {
                                    if (product.DateFifo != null)
                                    {
                                        result = false;
                                        context.MessageFormatter.AppendArgument("Index", index);
                                        context.MessageFormatter.AppendArgument("IndexProduct", indexProduct);
                                        context.MessageFormatter.AppendArgument("Key", nameof(product.DateFifo));
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
