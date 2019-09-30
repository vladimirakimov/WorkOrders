using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Application.Exceptions
{
    [Serializable]
    public class CommandVersionException : Exception
    {
        public CommandVersionException() : base(ExceptionMessage.CommandVersion) { }
        protected CommandVersionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
