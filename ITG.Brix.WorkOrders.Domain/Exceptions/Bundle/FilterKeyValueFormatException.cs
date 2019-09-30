using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class FilterKeyValueFormatException : DomainException
    {
        internal FilterKeyValueFormatException() : base(ExceptionMessage.FilterKeyValueFormat) { }
        internal FilterKeyValueFormatException(string message) : base(message) { }
        protected FilterKeyValueFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
