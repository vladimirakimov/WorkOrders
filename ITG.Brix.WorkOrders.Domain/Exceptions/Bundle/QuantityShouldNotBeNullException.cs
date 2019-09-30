using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class QuantityShouldNotBeNullException : DomainException
    {
        internal QuantityShouldNotBeNullException() : base(ExceptionMessage.QuantityShouldNotBeNull) { }
        protected QuantityShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
