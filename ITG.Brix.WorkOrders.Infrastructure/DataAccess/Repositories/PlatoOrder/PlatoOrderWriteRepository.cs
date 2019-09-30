using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Constants;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Extensions;
using MongoDB.Driver;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories
{
    public class PlatoOrderWriteRepository : IPlatoOrderWriteRepository
    {
        private readonly IMongoCollection<PlatoOrderClass> _collection;

        public PlatoOrderWriteRepository(IPersistenceContext persistenceContext)
        {
            Guard.On(persistenceContext, Error.ArgumentNull(nameof(persistenceContext))).AgainstNull();

            _collection = persistenceContext.Database.GetCollection<PlatoOrderClass>(Consts.Collections.PlatoOrders);
        }

        public async Task CreateAsync(PlatoOrder platoOrder)
        {
            try
            {
                var platoOrderClass = new PlatoOrderClass()
                {
                    Id = platoOrder.Id,
                    ReceivedOn = DateTime.UtcNow,
                    Content = platoOrder.XmlString
                };

                await _collection.InsertOneAsync(platoOrderClass);
            }
            catch (MongoWriteException ex)
            {
                if (ex.IsUniqueViolation())
                {
                    throw Error.UniqueKey(ex);
                }
                throw Error.GenericDb(ex);
            }
            catch (MongoCommandException ex)
            {
                Debug.WriteLine(ex);
                throw Error.GenericDb(ex);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }
    }
}
