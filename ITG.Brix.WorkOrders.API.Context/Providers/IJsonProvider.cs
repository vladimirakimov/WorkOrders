using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.API.Context.Providers
{
    public interface IJsonProvider
    {
        IDictionary<string, object> ToDictionary(string json);
    }
}
