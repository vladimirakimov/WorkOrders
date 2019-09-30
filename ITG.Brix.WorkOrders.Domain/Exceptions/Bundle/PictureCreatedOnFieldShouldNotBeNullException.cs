using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Domain.Exceptions
{
    [Serializable]
    public class PictureCreatedOnFieldShouldNotBeNullException : DomainException
    {
        internal PictureCreatedOnFieldShouldNotBeNullException() : base(ExceptionMessage.PictureCreatedOnFieldShouldNotBeNull) { }
        protected PictureCreatedOnFieldShouldNotBeNullException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
