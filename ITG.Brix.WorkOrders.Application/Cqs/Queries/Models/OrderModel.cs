using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Application.Cqs.Queries.Models
{
    public class OrderModel
    {
        public string Number { get; set; }
        public OriginModel Origin { get; set; }
        public CustomerModel Customer { get; set; }
        public CustomsModel Customs { get; set; }
        public TransportModel Transport { get; set; }
        public IEnumerable<UnitModel> Units { get; set; }
        public OperationModel Operation { get; set; }
    }
}
