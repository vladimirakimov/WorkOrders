using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class WarehouseLabelFieldShouldNotBeNullException : DomainException
    {
        internal WarehouseLabelFieldShouldNotBeNullException() : base(ExceptionMessage.WarehouseLabelFieldShouldNotBeNull) { }
        protected WarehouseLabelFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
