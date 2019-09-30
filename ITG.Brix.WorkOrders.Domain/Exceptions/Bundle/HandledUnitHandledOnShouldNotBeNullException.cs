
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class HandledUnitHandledOnShouldNotBeNullException : DomainException
    {
        internal HandledUnitHandledOnShouldNotBeNullException() : base(ExceptionMessage.HandledUnitHandledOnShouldNotBeNull) { }
        protected HandledUnitHandledOnShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
