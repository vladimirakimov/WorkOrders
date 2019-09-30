using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.Domain
{
    public class Order
    {
        public Order()
        {
            Origin = new Origin();
            Customer = new Customer();
            Customs = new Customs();
            Transport = new Transport();
            Units = new List<Unit>();
            Operation = new Operation();
        }
        public string Number { get; set; }
        public Origin Origin { get; set; }
        public Customer Customer { get; set; }
        public Customs Customs { get; set; }
        public Transport Transport { get; set; }
        public IEnumerable<Unit> Units { get; set; }
        public Operation Operation { get; set; }
    }
}
