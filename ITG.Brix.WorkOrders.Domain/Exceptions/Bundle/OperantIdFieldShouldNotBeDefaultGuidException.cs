using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class OperantIdFieldShouldNotBeDefaultGuidException : DomainException
    {
        internal OperantIdFieldShouldNotBeDefaultGuidException() : base(ExceptionMessage.OperantIdFieldShouldNotBeDefaultGuid) { }
        protected OperantIdFieldShouldNotBeDefaultGuidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
