using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface IWorkOrderProvider
    {
        IDictionary<string, string> GetPropertyTypePairs();
    }
}
