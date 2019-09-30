using System;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class RemarkClass
    {
        public Guid OperantId { get; set; }
        public string OperantLogin { get; set; }
        public long CreatedOn { get; set; }
        public string Text { get; set; }
    }
}
