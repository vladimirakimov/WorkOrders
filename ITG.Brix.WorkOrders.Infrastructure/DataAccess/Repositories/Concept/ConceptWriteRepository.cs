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
    public class ConceptWriteRepository : IConceptWriteRepository
    {
        private readonly IMongoCollection<ConceptClass> _collection;

        public ConceptWriteRepository(IPersistenceContext persistenceContext)
        {
            Guard.On(persistenceContext, Error.ArgumentNull(nameof(persistenceContext))).AgainstNull();


            _collection = persistenceContext.Database.GetCollection<ConceptClass>(Consts.Collections.Concepts);
        }

        public async Task CreateAsync(Concept concept)
        {
            try
            {
                var conceptClass = new ConceptClass() { Id = Guid.NewGuid(), CreatedOn = 44, Version = 1 };

                await _collection.InsertOneAsync(conceptClass);
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
