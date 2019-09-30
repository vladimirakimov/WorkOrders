using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using MongoDB.Driver;
using System;
using System.Security.Authentication;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl
{
    public class PersistenceContext : IPersistenceContext
    {
        private readonly IPersistenceConfiguration _persistenceConfiguration;
        private IMongoDatabase _database;

        public PersistenceContext(IPersistenceConfiguration persistenceConfiguration)
        {
            Guard.On(persistenceConfiguration, Error.ArgumentNull(nameof(persistenceConfiguration))).AgainstNull();

            _persistenceConfiguration = persistenceConfiguration ?? throw new ArgumentNullException(nameof(persistenceConfiguration));
        }

        public IMongoDatabase Database
        {
            get
            {
                if (_database == null)
                {
                    MongoClientSettings settings = MongoClientSettings.FromUrl(
                      new MongoUrl(_persistenceConfiguration.ConnectionString)
                    );

                    settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };

                    var client = new MongoClient(settings);

                    _database = client.GetDatabase(_persistenceConfiguration.Database);
                }
                return _database;
            }
        }
    }
}
