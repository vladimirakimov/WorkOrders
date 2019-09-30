namespace ITG.Brix.WorkOrders.Domain
{
    public class Path
    {
        public Path(FilterKey filterKey, string property)
        {
            FilterKey = filterKey;
            Property = property;
        }

        public FilterKey FilterKey { get; }
        public string Property { get; }
    }
}
