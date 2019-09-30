using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class DateOnValueFieldShouldBeValidException : DomainException
    {
        internal DateOnValueFieldShouldBeValidException() : base(ExceptionMessage.DateOnValueFieldShouldBeValid) { }
        protected DateOnValueFieldShouldBeValidException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
