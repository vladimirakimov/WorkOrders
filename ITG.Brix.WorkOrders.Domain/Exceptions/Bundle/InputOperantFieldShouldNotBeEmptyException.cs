using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class InputOperantFieldShouldNotBeEmptyException : DomainException
    {
        internal InputOperantFieldShouldNotBeEmptyException() : base(ExceptionMessage.InputOperantFieldShouldNotBeEmpty) { }
        protected InputOperantFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
