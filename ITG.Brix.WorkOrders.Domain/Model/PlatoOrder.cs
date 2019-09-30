using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;

namespace ITG.Brix.WorkOrders.Domain
{
    public class PlatoOrder : Entity
    {
        public string XmlString { get; private set; }

        public PlatoOrder(Guid id, string xmlString) : base(id)
        {
            if (string.IsNullOrEmpty(xmlString))
            {
                throw Error.ArgumentNull(string.Format("{0} can't be null or empty", nameof(xmlString)));
            }

            XmlString = xmlString;
        }
    }
}
