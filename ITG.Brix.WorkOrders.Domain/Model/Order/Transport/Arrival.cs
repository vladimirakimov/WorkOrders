namespace ITG.Brix.WorkOrders.Domain
{
    public class Arrival
    {
        public DateOn Expected { get; set; }
        public DateOn Latest { get; set; }
        public DateOn Arrived { get; set; }
    }

}
