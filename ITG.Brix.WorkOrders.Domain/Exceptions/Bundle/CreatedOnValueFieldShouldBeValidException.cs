using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class CreatedOnValueFieldShouldBeValidException : DomainException
    {
        internal CreatedOnValueFieldShouldBeValidException() : base(ExceptionMessage.CreatedOnValueFieldShouldBeValid) { }
        protected CreatedOnValueFieldShouldBeValidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
