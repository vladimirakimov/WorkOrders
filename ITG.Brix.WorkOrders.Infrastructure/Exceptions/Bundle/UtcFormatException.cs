using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class UtcFormatException : Exception
    {
        public UtcFormatException() : base("UtcFormat: must be yyyy-MM-ddThh:mm:ssZ") { }

        internal UtcFormatException(string message) : base("UtcFormat: must be yyyy-MM-ddThh:mm:ssZ, for value '" + message + "'") { }

        internal UtcFormatException(Exception ex) : base(ex.Message, ex) { }

        protected UtcFormatException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
