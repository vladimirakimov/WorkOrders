using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class InputValueFieldShouldNotBeEmptyException : DomainException
    {
        internal InputValueFieldShouldNotBeEmptyException() : base(ExceptionMessage.InputValueFieldShouldNotBeEmpty) { }
        protected InputValueFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
