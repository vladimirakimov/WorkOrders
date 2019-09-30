using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class HandledOnValueFieldShouldBeValidException : DomainException
    {
        internal HandledOnValueFieldShouldBeValidException() : base(ExceptionMessage.HandledOnValueFieldShouldBeValid) { }
        protected HandledOnValueFieldShouldBeValidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
