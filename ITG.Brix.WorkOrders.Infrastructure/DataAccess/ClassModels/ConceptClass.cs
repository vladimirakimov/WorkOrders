using System;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class ConceptClass
    {
        public Guid Id { get; set; }
        public int Version { get; set; }
        public long? CreatedOn { get; set; }
    }
}
