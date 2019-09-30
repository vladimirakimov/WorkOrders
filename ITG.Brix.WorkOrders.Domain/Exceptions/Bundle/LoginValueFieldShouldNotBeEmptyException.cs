﻿using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class LoginValueFieldShouldNotBeEmptyException : DomainException
    {
        internal LoginValueFieldShouldNotBeEmptyException() : base(ExceptionMessage.LoginValueFieldShouldNotBeEmpty) { }
        protected LoginValueFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
