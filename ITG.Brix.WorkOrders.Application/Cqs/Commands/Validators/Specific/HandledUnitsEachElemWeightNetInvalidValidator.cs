using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class HandledUnitsEachElemWeightNetInvalidValidator : PropertyValidator
    {
        public HandledUnitsEachElemWeightNetInvalidValidator() : base("HandledUnits[{Index}].WeightNet has invalid value.") { }

        protected override bool IsValid(PropertyValidatorContext context)
        {
            var result = true;
            var handledUnits = (Optional<IEnumerable<HandledUnitDto>>)context.PropertyValue;
            if (handledUnits.HasValue && handledUnits.Value != null && handledUnits.Value.Any())
            {
                var index = 0;
                foreach (var handledUnit in handledUnits.Value)
                {
                    if (handledUnit != null)
                    {
                        var resultConvertion = float.TryParse(handledUnit.WeightNet, out float netWeight);
                        if (resultConvertion)
                        {
                            try
                            {
                                new Weight(netWeight);
                            }
                            catch
                            {
                                result = false;
                                context.MessageFormatter.AppendArgument("Key", nameof(handledUnit.WeightNet));
                                context.MessageFormatter.AppendArgument("Index", index);
                            }
                        }
                        else
                        {
                            result = false;
                            context.MessageFormatter.AppendArgument("Key", nameof(handledUnit.WeightNet));
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
