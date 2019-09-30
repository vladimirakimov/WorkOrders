using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class CreatedOnShouldNotBeNullException : DomainException
    {
        internal CreatedOnShouldNotBeNullException() : base(ExceptionMessage.CreatedOnShouldNotBeNull) { }
        protected CreatedOnShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
