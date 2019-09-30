using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class WorkOrdersModel
    {
        public long Count { get; set; }
        public string NextLink { get; set; }
        public IEnumerable<WorkOrderModel> Value { get; set; }
    }
}
