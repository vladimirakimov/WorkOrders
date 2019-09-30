using FluentValidation;
using FluentValidation.Results;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Enums;
using ITG.Brix.WorkOrders.Application.Extensions;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain;
using System;
using System.Linq;

namespace ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators
{
    public class UpdateWorkOrderCommandValidator : AbstractValidator<UpdateWorkOrderCommand>
    {
        public UpdateWorkOrderCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().CustomFault(CustomFaultCode.NotFound, CustomFailures.WorkOrderNotFound);

            #region Status

            RuleFor(x => x.Status).Custom((elem, context) =>
            {
                if (elem.HasValue && string.IsNullOrWhiteSpace(elem.Value))
                {
                    context.AddFailure(new ValidationFailure("Status", ValidationFailures.StatusCannotBeEmpty) { ErrorCode = ErrorType.ValidationError.ToString() });
                }
            });

            RuleFor(x => x.Status).Custom((elem, context) =>
            {
                if (elem.HasValue && !string.IsNullOrWhiteSpace(elem.Value))
                {
                    try
                    {
                        Status.Parse(elem.Value);
                    }
                    catch (ArgumentException)
                    {
                        var message = string.Format(ValidationFailures.StatusAllowedValues, String.Join(", ", Status.List().Select(s => s.Name)));
                        context.AddFailure(new ValidationFailure("Status", message) { ErrorCode = ErrorType.ValidationError.ToString() });
                    }
                }
            });

            #endregion

            #region HandledUnits

            RuleFor(x => x.HandledUnits).OptNotEmpty().ValidationFault(ValidationFailures.HandledUnitsCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemNotNull().ValidationFault(ValidationFailures.HandledUnitsElemCannotBeNull);

            RuleFor(x => x.HandledUnits).OptEachElemIdNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemIdInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemSourceUnitIdNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemSourceUnitIdInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemOperantIdNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemOperantIdInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemOperantLoginNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);


            RuleFor(x => x.HandledUnits).OptEachElemWarehouseNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemGateNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemRowNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemPositionNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);

            RuleFor(x => x.HandledUnits).OptEachElemUnitsNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemUnitsInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemIsPartialNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemIsPartialInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemIsMixedNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemIsMixedInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemQuantityNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemQuantityInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemWeightNetNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemWeightNetInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemWeightGrossNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemWeightGrossInvalid().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueInvalid);


            RuleFor(x => x.HandledUnits).OptEachElemProductsNotEmpty().ValidationFault(ValidationFailures.HandledUnitsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemNotNull().ValidationFault(ValidationFailures.HandledUnitsProductsElemCannotBeNull);

            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemBestBeforeDateInvalid().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid);
            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemDateFifoInvalid().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemQuantityNotEmpty().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemQuantityInvalid().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemWeightNetNotEmpty().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemWeightNetInvalid().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid);

            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemWeightGrossNotEmpty().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.HandledUnits).OptEachElemProductsEachElemWeightGrossInvalid().ValidationFault(ValidationFailures.HandledUnitsProductsElemKeyValueInvalid);

            #endregion

            #region Remarks

            RuleFor(x => x.Remarks).OptNotEmpty().ValidationFault(ValidationFailures.RemarksCannotBeEmpty);
            RuleFor(x => x.Remarks).OptEachElemNotNull().ValidationFault(ValidationFailures.RemarksElemCannotBeNull);

            RuleFor(x => x.Remarks).OptEachElemOperantIdNotEmpty().ValidationFault(ValidationFailures.RemarksElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Remarks).OptEachElemOperantIdInvalid().ValidationFault(ValidationFailures.RemarksElemKeyValueInvalid);

            RuleFor(x => x.Remarks).OptEachElemOperantNotEmpty().ValidationFault(ValidationFailures.RemarksElemKeyValueCannotBeEmpty);

            RuleFor(x => x.Remarks).OptEachElemCreatedOnNotEmpty().ValidationFault(ValidationFailures.RemarksElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Remarks).OptEachElemCreatedOnInvalid().ValidationFault(ValidationFailures.RemarksElemKeyValueInvalid);

            RuleFor(x => x.Remarks).OptEachElemTextNotEmpty().ValidationFault(ValidationFailures.RemarksElemKeyValueCannotBeEmpty);

            #endregion

            #region Pictures

            RuleFor(x => x.Pictures).OptNotEmpty().ValidationFault(ValidationFailures.PicturesCannotBeEmpty);
            RuleFor(x => x.Pictures).OptEachElemNotNull().ValidationFault(ValidationFailures.PicturesElemCannotBeNull);

            RuleFor(x => x.Pictures).OptEachElemOperantIdNotEmpty().ValidationFault(ValidationFailures.PicturesElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Pictures).OptEachElemOperantIdInvalid().ValidationFault(ValidationFailures.PicturesElemKeyValueInvalid);

            RuleFor(x => x.Pictures).OptEachElemOperantNotEmpty().ValidationFault(ValidationFailures.PicturesElemKeyValueCannotBeEmpty);

            RuleFor(x => x.Pictures).OptEachElemCreatedOnNotEmpty().ValidationFault(ValidationFailures.PicturesElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Pictures).OptEachElemCreatedOnInvalid().ValidationFault(ValidationFailures.PicturesElemKeyValueInvalid);

            RuleFor(x => x.Pictures).OptEachElemNameNotEmpty().ValidationFault(ValidationFailures.PicturesElemKeyValueCannotBeEmpty);

            #endregion

            #region Inputs
            RuleFor(x => x.Inputs).OptNotEmpty().ValidationFault(ValidationFailures.InputsCannotBeEmpty);
            RuleFor(x => x.Inputs).OptEachElemNotNull().ValidationFault(ValidationFailures.InputsElemCannotBeNull);

            RuleFor(x => x.Inputs).OptEachElemOperantIdNotEmpty().ValidationFault(ValidationFailures.InputsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Inputs).OptEachElemOperantIdInvalid().ValidationFault(ValidationFailures.InputsElemKeyValueInvalid);

            RuleFor(x => x.Inputs).OptEachElemOperantNotEmpty().ValidationFault(ValidationFailures.InputsElemKeyValueCannotBeEmpty);

            RuleFor(x => x.Inputs).OptEachElemCreatedOnNotEmpty().ValidationFault(ValidationFailures.InputsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Inputs).OptEachElemCreatedOnInvalid().ValidationFault(ValidationFailures.InputsElemKeyValueInvalid);

            RuleFor(x => x.Inputs).OptEachElemPropertyNotEmpty().ValidationFault(ValidationFailures.InputsElemKeyValueCannotBeEmpty);
            RuleFor(x => x.Inputs).OptEachElemValueNotEmpty().ValidationFault(ValidationFailures.InputsElemKeyValueCannotBeEmpty);
            #endregion
        }
    }
}
