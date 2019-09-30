using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class RemarkCreatedOnFieldShouldNotBeNullException : DomainException
    {
        internal RemarkCreatedOnFieldShouldNotBeNullException() : base(ExceptionMessage.RemarkCreatedOnFieldShouldNotBeNull) { }
        protected RemarkCreatedOnFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
