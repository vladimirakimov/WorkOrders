using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class ItemValueNullOrWhitespaceException : DomainException
    {
        internal ItemValueNullOrWhitespaceException() : base(ExceptionMessage.ItemValueNullOrWhitespace) { }
        protected ItemValueNullOrWhitespaceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
