using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl
{
    public class PersistenceConfiguration : IPersistenceConfiguration
    {
        private readonly string _connectionString;

        public PersistenceConfiguration(string connectionString)
        {
            Guard.On(connectionString, Error.ArgumentNull(nameof(connectionString))).AgainstNull();

            _connectionString = connectionString;
        }

        public string ConnectionString => _connectionString;

        public string Database => "Brix-WorkOrders";
    }
}
