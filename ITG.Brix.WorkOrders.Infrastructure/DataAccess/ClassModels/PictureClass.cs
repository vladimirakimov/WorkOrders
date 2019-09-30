using System;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels
{
    public class PictureClass
    {
        public Guid OperantId { get; set; }
        public string OperantLogin { get; set; }
        public string Name { get; set; }
        public long CreatedOn { get; set; }
    }
}
