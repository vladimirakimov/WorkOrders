
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class UnitLocationShouldNotBeNullException : DomainException
    {
        internal UnitLocationShouldNotBeNullException() : base(ExceptionMessage.UnitLocationShouldNotBeNull) { }
        protected UnitLocationShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
