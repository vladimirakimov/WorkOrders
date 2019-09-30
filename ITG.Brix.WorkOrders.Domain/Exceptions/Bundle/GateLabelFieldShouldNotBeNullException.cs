using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class GateLabelFieldShouldNotBeNullException : DomainException
    {
        internal GateLabelFieldShouldNotBeNullException() : base(ExceptionMessage.GateLabelFieldShouldNotBeNull) { }
        protected GateLabelFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
