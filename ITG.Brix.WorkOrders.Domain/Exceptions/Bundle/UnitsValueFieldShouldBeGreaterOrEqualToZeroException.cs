using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class UnitsValueFieldShouldBeGreaterOrEqualToZeroException : DomainException
    {
        internal UnitsValueFieldShouldBeGreaterOrEqualToZeroException() : base(ExceptionMessage.UnitsValueFieldShouldBeGreaterOrEqualToZero) { }
        protected UnitsValueFieldShouldBeGreaterOrEqualToZeroException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
