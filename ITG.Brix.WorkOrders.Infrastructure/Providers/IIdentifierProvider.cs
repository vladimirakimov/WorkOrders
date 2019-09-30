using System;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface IIdentifierProvider
    {
        Guid Generate();
    }
}
