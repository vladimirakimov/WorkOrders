
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class HandledUnitOperantShouldNotBeNullException : DomainException
    {
        internal HandledUnitOperantShouldNotBeNullException() : base(ExceptionMessage.HandledUnitOperantShouldNotBeNull) { }
        protected HandledUnitOperantShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
