﻿using FluentValidation.Results;
using FluentValidation.Validators;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Enums;

namespace ITG.Brix.WorkOrders.Application.Extensions
{
    public static class CustomContextExtensions
    {
        public static void AddValidationFault(this CustomContext context, string property, string errorMessage)
        {
            context.AddFailure(new ValidationFailure(property, errorMessage) { ErrorCode = ErrorType.ValidationError.ToString() });
        }

        public static void AddCustomFault(this CustomContext context, string property, CustomFaultCode errorCode, string errorMessage)
        {
            context.AddFailure(new ValidationFailure(property, errorMessage) { ErrorCode = ErrorType.CustomError.ToString() + "###" + errorCode.Name });
        }
    }
}
