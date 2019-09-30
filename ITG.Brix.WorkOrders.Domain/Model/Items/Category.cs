namespace ITG.Brix.WorkOrders.Domain
{
    public class Category
    {
        public Category(GeneralGroup general, TeamFilterGroup teamFilter, ProductGroup product)
        {
            General = general;
            TeamFilter = teamFilter;
            Product = product;
        }

        public GeneralGroup General { get; }
        public TeamFilterGroup TeamFilter { get; }
        public ProductGroup Product { get; }
    }
}
