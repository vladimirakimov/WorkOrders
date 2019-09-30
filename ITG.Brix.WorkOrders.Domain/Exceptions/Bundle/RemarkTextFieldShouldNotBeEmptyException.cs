using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class RemarkTextFieldShouldNotBeEmptyException : DomainException
    {
        internal RemarkTextFieldShouldNotBeEmptyException() : base(ExceptionMessage.RemarkTextFieldShouldNotBeEmpty) { }
        protected RemarkTextFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
