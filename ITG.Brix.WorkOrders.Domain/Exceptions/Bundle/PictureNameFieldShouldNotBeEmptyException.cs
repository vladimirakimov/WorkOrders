using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class PictureNameFieldShouldNotBeEmptyException : DomainException
    {
        internal PictureNameFieldShouldNotBeEmptyException() : base(ExceptionMessage.PictureNameFieldShouldNotBeEmpty) { }
        protected PictureNameFieldShouldNotBeEmptyException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
