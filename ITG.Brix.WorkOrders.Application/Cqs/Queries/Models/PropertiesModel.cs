using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class PropertiesModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<PropertyModel> Value { get; set; }
    }
}
