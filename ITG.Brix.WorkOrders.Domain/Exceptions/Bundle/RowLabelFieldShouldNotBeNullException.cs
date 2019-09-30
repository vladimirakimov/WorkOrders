using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class RowLabelFieldShouldNotBeNullException : DomainException
    {
        internal RowLabelFieldShouldNotBeNullException() : base(ExceptionMessage.RowLabelFieldShouldNotBeNull) { }
        protected RowLabelFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
