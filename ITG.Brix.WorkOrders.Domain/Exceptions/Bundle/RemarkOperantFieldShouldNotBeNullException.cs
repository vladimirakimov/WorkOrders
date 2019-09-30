using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class RemarkOperantFieldShouldNotBeNullException : DomainException
    {
        internal RemarkOperantFieldShouldNotBeNullException() : base(ExceptionMessage.RemarkOperantFieldShouldNotBeNull) { }
        protected RemarkOperantFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
