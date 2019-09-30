using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Infrastructure.Providers.Impl
{
    public class FilterProvider : IFilterProvider
    {
        public string Replace(string filter, IDictionary<string, string> replacements)
        {
            string result = null;
            if (!string.IsNullOrWhiteSpace(filter))
            {
                result = filter;
                foreach (var replacement in replacements)
                {
                    result = result.Replace(replacement.Key + " ", replacement.Value + " ");
                }
            }
            return result;
        }
    }
}
