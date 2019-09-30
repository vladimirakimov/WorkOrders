using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using System;

namespace ITG.Brix.WorkOrders.Infrastructure.Orchestrations.Impl
{
    public class BiztalkOrchestration : IBiztalkOrchestration
    {
        public void Acknowledge(string content)
        {
            string nackMarker = "<NAckID>";
            bool contains = content.IndexOf(nackMarker, StringComparison.InvariantCultureIgnoreCase) >= 0;
            if (contains)
            {
                var exception = new Exception(content);
                throw new PlatoCallException(exception);
            }
        }
    }
}
