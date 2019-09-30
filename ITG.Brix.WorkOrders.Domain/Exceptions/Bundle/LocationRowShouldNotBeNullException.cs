using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class LocationRowShouldNotBeNullException : DomainException
    {
        internal LocationRowShouldNotBeNullException() : base(ExceptionMessage.LocationRowShouldNotBeNull) { }
        protected LocationRowShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
