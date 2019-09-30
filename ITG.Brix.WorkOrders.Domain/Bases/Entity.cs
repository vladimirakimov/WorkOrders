using ITG.Brix.WorkOrders.Domain.Exceptions;
using System;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Entity
    {
        public Guid Id { get; protected set; }

        public Entity(Guid id)
        {
            if (id == default(Guid))
            {
                throw Error.IdFieldShouldNotBeDefaultGuid();
            }

            Id = id;
        }
    }
}
