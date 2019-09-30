using System;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class WorkOrderModel
    {
        public Guid Id { get; set; }
        public bool IsEditable { get; set; }
        public string UserCreated { get; set; }
        public string CreatedOn { get; set; }
        public OrderModel Order { get; set; }
        public OperationalModel Operational { get; set; }
    }
}
