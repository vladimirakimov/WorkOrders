using System;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class PlatoOrderClass
    {
        public Guid Id { get; set; }
        public DateTime ReceivedOn { get; set; }
        public string Content { get; set; }
    }
}
