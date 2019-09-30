using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class IdFieldShouldNotBeDefaultGuidException : DomainException
    {
        internal IdFieldShouldNotBeDefaultGuidException() : base(ExceptionMessage.IdFieldShouldNotBeDefaultGuid) { }
        protected IdFieldShouldNotBeDefaultGuidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
