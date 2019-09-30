using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class EntityVersionDbException : Exception
    {
        public EntityVersionDbException() : base(ExceptionMessage.EntityVersionDb) { }

        protected EntityVersionDbException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
