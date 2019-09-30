using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class BiztalkCallException : Exception
    {
        public BiztalkCallException() : base(ExceptionMessage.BiztalkCall) { }

        internal BiztalkCallException(Exception ex) : base(ex.Message, ex) { }

        protected BiztalkCallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
