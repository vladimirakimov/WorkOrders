using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class LabelValueFieldShouldNotBeEmptyException : DomainException
    {
        internal LabelValueFieldShouldNotBeEmptyException() : base(ExceptionMessage.LabelValueFieldShouldNotBeEmpty) { }
        protected LabelValueFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
