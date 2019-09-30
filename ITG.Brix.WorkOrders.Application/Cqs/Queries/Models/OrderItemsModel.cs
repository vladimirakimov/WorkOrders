using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class OrderItemsModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<OrderItemModel> Value { get; set; }
    }
}
