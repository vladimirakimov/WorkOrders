using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class UnitsShouldNotBeNullException : DomainException
    {
        internal UnitsShouldNotBeNullException() : base(ExceptionMessage.UnitsShouldNotBeNull) { }
        protected UnitsShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
