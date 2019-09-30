using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class LocationWarehouseShouldNotBeNullException : DomainException
    {
        internal LocationWarehouseShouldNotBeNullException() : base(ExceptionMessage.LocationWarehouseShouldNotBeNull) { }
        protected LocationWarehouseShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
