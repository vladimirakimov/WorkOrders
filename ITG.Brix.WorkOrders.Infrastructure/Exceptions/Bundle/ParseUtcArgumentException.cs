using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class ParseUtcArgumentException : Exception
    {
        public ParseUtcArgumentException() : base(ExceptionMessage.ParseUtcArgument) { }

        protected ParseUtcArgumentException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
