using FluentValidation;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators;
using ITG.Brix.WorkOrders.Application.DataTypes;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Extensions
{
    public static class IRuleBuilderExtensions
    {
        #region HandledUnits

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemNotNull<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemNotNullValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemIdNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemIdNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemIdInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemIdInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemSourceUnitIdNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemSourceUnitIdNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemSourceUnitIdInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemSourceUnitIdInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemOperantIdNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemOperantIdNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemOperantIdInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemOperantIdInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemOperantLoginNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemOperantLoginNotEmptyValidator());
        }


        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemWarehouseNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemWarehouseNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemGateNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemGateNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemRowNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemRowNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemPositionNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemPositionNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemUnitsNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemUnitsNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemUnitsInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemUnitsInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemIsPartialNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemIsPartialNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemIsPartialInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemIsPartialInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemIsMixedNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemIsMixedNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemIsMixedInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemIsMixedInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemQuantityNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemQuantityNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemQuantityInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemQuantityInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemWeightNetNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemWeightNetNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemWeightNetInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemWeightNetInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemWeightGrossNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemWeightGrossNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemWeightGrossInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemWeightGrossInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemNotNull<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemNotNullValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemBestBeforeDateInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemBestBeforeDateInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemDateFifoInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemDateFifoInvalidValidator());
        }
        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemQuantityNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemQuantityNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemQuantityInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemQuantityInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemWeightNetNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemWeightNetNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemWeightNetInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemWeightNetInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemWeightGrossNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemWeightGrossNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<HandledUnitDto>>> OptEachElemProductsEachElemWeightGrossInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<HandledUnitDto>>> rule)
        {
            return rule.SetValidator(new HandledUnitsEachElemProductsEachElemWeightGrossInvalidValidator());
        }

        #endregion

        #region Remarks

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemNotNull<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemNotNullValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemOperantIdNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemOperantIdNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemOperantIdInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemOperantIdInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemOperantNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemOperantNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemCreatedOnNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemCreatedOnNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemCreatedOnInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemCreatedOnInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<RemarkDto>>> OptEachElemTextNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<RemarkDto>>> rule)
        {
            return rule.SetValidator(new RemarksEachElemTextNotEmptyValidator());
        }

        #endregion

        #region Pictures

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemNotNull<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemNotNullValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemOperantIdNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemOperantIdNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemOperantIdInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemOperantIdInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemOperantNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemOperantNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemCreatedOnNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemCreatedOnNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemCreatedOnInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemCreatedOnInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<PictureDto>>> OptEachElemNameNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<PictureDto>>> rule)
        {
            return rule.SetValidator(new PicturesEachElemNameNotEmptyValidator());
        }

        #endregion

        #region Inputs
        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemNotNull<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemNotNullValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemOperantIdNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemOperantIdNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemOperantIdInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemOperantIdInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemOperantNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemOperantNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemCreatedOnNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemCreatedOnNotEmptyValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemCreatedOnInvalid<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemCreatedOnInvalidValidator());
        }

        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemPropertyNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemPropertyNotEmptyValidator());
        }
        public static IRuleBuilderOptions<T, Optional<IEnumerable<InputDto>>> OptEachElemValueNotEmpty<T>(this IRuleBuilder<T, Optional<IEnumerable<InputDto>>> rule)
        {
            return rule.SetValidator(new InputsEachElemValueNotEmptyValidator());
        }
        #endregion
    }
}
