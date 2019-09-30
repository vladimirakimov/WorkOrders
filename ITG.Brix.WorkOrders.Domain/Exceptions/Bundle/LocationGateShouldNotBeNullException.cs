using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class LocationGateShouldNotBeNullException : DomainException
    {
        internal LocationGateShouldNotBeNullException() : base(ExceptionMessage.LocationGateShouldNotBeNull) { }
        protected LocationGateShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
