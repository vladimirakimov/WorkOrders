
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class HandledUnitLocationShouldNotBeNullException : DomainException
    {
        internal HandledUnitLocationShouldNotBeNullException() : base(ExceptionMessage.HandledUnitLocationShouldNotBeNull) { }
        protected HandledUnitLocationShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
