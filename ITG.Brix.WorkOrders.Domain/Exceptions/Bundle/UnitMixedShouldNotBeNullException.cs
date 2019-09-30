
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class UnitMixedShouldNotBeNullException : DomainException
    {
        internal UnitMixedShouldNotBeNullException() : base(ExceptionMessage.UnitMixedShouldNotBeNull) { }
        protected UnitMixedShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
