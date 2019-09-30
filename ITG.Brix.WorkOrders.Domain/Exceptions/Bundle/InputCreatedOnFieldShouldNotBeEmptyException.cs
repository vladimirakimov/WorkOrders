using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class InputCreatedOnFieldShouldNotBeEmptyException : DomainException
    {
        internal InputCreatedOnFieldShouldNotBeEmptyException() : base(ExceptionMessage.InputCreatedOnFieldShouldNotBeEmpty) { }
        protected InputCreatedOnFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
