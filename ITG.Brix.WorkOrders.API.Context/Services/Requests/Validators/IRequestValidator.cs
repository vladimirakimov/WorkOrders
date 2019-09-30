﻿using ITG.Brix.WorkOrders.API.Context.Bases;
using System;

namespace ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators
{
    public interface IRequestValidator
    {
        ValidationResult Validate<T>(T request);

        Type Type { get; }
    }
}
