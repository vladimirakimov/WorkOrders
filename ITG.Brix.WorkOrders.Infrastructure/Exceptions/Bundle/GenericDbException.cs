using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class GenericDbException : Exception
    {
        public GenericDbException(Exception ex) : base(ExceptionMessage.GenericDb, ex) { }

        protected GenericDbException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
