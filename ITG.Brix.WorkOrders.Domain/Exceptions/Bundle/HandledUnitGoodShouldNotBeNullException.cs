
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class HandledUnitGoodShouldNotBeNullException : DomainException
    {
        internal HandledUnitGoodShouldNotBeNullException() : base(ExceptionMessage.HandledUnitGoodShouldNotBeNull) { }
        protected HandledUnitGoodShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
