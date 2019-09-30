using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class LocationPositionShouldNotBeNullException : DomainException
    {
        internal LocationPositionShouldNotBeNullException() : base(ExceptionMessage.LocationPositionShouldNotBeNull) { }
        protected LocationPositionShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
