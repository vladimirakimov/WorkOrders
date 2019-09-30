using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class OperantLoginFieldShouldNotBeNullException : DomainException
    {
        internal OperantLoginFieldShouldNotBeNullException() : base(ExceptionMessage.OperantLoginFieldShouldNotBeNull) { }
        protected OperantLoginFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
