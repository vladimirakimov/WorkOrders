using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class UniqueKeyDbException : Exception
    {
        public UniqueKeyDbException() : base(ExceptionMessage.UniqueKeyDb) { }

        public UniqueKeyDbException(Exception ex) : base(ex.Message, ex) { }

        protected UniqueKeyDbException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
