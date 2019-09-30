using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class HandledOnShouldNotBeNullException : DomainException
    {
        internal HandledOnShouldNotBeNullException() : base(ExceptionMessage.HandledOnShouldNotBeNull) { }
        protected HandledOnShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
