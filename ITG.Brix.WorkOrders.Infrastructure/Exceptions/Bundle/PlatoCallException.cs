using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Infrastructure.Exceptions
{
    [Serializable]
    public class PlatoCallException : Exception
    {
        public PlatoCallException() : base(ExceptionMessage.PlatoCall) { }

        internal PlatoCallException(Exception ex) : base(ex.Message, ex) { }

        protected PlatoCallException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
