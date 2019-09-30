
using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class UnitGroupShouldNotBeNullException : DomainException
    {
        internal UnitGroupShouldNotBeNullException() : base(ExceptionMessage.UnitGroupShouldNotBeNull) { }
        protected UnitGroupShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
