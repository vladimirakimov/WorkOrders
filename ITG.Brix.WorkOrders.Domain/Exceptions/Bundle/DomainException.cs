using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        internal DomainException(string message) : base(message) { }
        protected DomainException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
