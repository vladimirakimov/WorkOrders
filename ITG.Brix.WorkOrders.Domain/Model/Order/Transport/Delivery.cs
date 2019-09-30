namespace ITG.Brix.WorkOrders.Domain
{
    public class Delivery
    {
        public DateOn Eta { get; set; }
        public DateOn Lta { get; set; }
        public string Place { get; set; }
        public string Reference { get; set; }
    }
}
