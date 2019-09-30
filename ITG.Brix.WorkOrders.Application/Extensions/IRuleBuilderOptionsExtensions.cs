using FluentValidation;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Enums;

namespace ITG.Brix.WorkOrders.Application.Extensions
{
    public static class IRuleBuilderOptionsExtensions
    {
        public static IRuleBuilderOptions<T, TProperty> CustomFault<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, CustomFaultCode errorCode, string errorMessage)
        {
            return rule.WithMessage(errorMessage).WithErrorCode(ErrorType.CustomError.ToString() + "###" + errorCode.Name);
        }

        public static IRuleBuilderOptions<T, TProperty> ValidationFault<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessage)
        {
            return rule.WithMessage(errorMessage);
        }
    }
}
