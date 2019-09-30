using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class WeightValueFieldShouldBeGreaterOrEqualToZeroException : DomainException
    {
        internal WeightValueFieldShouldBeGreaterOrEqualToZeroException() : base(ExceptionMessage.WeightValueFieldShouldBeGreaterOrEqualToZero) { }
        protected WeightValueFieldShouldBeGreaterOrEqualToZeroException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
