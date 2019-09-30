using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class FilterKeyValueNullOrWhitespaceException : DomainException
    {
        internal FilterKeyValueNullOrWhitespaceException() : base(ExceptionMessage.FilterKeyValueNullOrWhitespace) { }
        protected FilterKeyValueNullOrWhitespaceException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
