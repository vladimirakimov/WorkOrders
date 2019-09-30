namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations
{
    public interface IPersistenceConfiguration
    {
        string ConnectionString { get; }
        string Database { get; }
    }
}
