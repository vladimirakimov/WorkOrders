using MongoDB.Driver;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations
{
    public interface IPersistenceContext
    {
        IMongoDatabase Database { get; }
    }
}
