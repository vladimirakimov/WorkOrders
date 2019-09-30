using System;
using System.Runtime.Serialization;

namespace ITG.Brix.WorkOrders.Application.Exceptions
{
    [Serializable]
    public class PlatoOrderOverviewCheckException : Exception
    {
        public PlatoOrderOverviewCheckException() : base(ExceptionMessage.PlatoOrderOverviewCheck) { }

        internal PlatoOrderOverviewCheckException(string message) : base("Cannot receive plato overview order." + Environment.NewLine + message) { }

        protected PlatoOrderOverviewCheckException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
