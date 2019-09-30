using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class EntityNotFoundDbException : Exception
    {
        public EntityNotFoundDbException() : base(ExceptionMessage.EntityNotFoundDb) { }

        protected EntityNotFoundDbException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
