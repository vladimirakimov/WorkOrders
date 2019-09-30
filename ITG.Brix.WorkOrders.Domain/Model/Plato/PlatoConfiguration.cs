namespace ITG.Brix.WorkOrders.Domain
{
    public class PlatoConfiguration
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public PlatoConfigurationUnit ConfigurationUnit { get; set; }
    }
}
