using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class InputPropertyFieldShouldNotBeEmptyException : DomainException
    {
        internal InputPropertyFieldShouldNotBeEmptyException() : base(ExceptionMessage.InputPropertyFieldShouldNotBeEmpty) { }
        protected InputPropertyFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
