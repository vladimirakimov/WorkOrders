using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class PictureOperantFieldShouldNotBeNullException : DomainException
    {
        internal PictureOperantFieldShouldNotBeNullException() : base(ExceptionMessage.PictureOperantFieldShouldNotBeNull) { }
        protected PictureOperantFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
