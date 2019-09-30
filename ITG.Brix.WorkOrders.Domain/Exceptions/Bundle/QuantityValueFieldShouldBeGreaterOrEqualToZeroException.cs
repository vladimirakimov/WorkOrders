using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class QuantityValueFieldShouldBeGreaterOrEqualToZeroException : DomainException
    {
        internal QuantityValueFieldShouldBeGreaterOrEqualToZeroException() : base(ExceptionMessage.QuantityValueFieldShouldBeGreaterOrEqualToZero) { }
        protected QuantityValueFieldShouldBeGreaterOrEqualToZeroException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
