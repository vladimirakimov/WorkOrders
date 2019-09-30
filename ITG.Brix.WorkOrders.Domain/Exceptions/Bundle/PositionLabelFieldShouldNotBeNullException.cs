using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class PositionLabelFieldShouldNotBeNullException : DomainException
    {
        internal PositionLabelFieldShouldNotBeNullException() : base(ExceptionMessage.PositionLabelFieldShouldNotBeNull) { }
        protected PositionLabelFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
