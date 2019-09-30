using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers
{
    public interface IFilterProvider
    {
        string Replace(string filter, IDictionary<string, string> replacements);
    }
}
