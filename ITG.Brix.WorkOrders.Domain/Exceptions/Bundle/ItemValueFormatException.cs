using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class ItemValueFormatException : DomainException
    {
        internal ItemValueFormatException() : base(ExceptionMessage.ItemValueFormat) { }
        internal ItemValueFormatException(string message) : base(message) { }
        protected ItemValueFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
