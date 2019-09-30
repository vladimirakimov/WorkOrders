using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Constants;
using ITG.Brix.WorkOrders.Infrastructure.Converters;
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
    public class WorkOrderWriteRepository : IWorkOrderWriteRepository
    {
        private readonly IModelConverter _modelConverter;

        private readonly IMongoCollection<WorkOrderClass> _collection;

        public WorkOrderWriteRepository(IPersistenceContext persistenceContext,
                                        IModelConverter modelConverter)
        {
            Guard.On(persistenceContext, Error.ArgumentNull(nameof(persistenceContext))).AgainstNull();
            Guard.On(modelConverter, Error.ArgumentNull(nameof(modelConverter))).AgainstNull();

            _modelConverter = modelConverter;

            _collection = persistenceContext.Database.GetCollection<WorkOrderClass>(Consts.Collections.WorkOrders);
        }

        public async Task CreateAsync(WorkOrder workOrder)
        {
            try
            {
                var workOrderClass = _modelConverter.ToClass(workOrder);

                await _collection.InsertOneAsync(workOrderClass);
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

        public async Task UpdateAsync(WorkOrder workOrder)
        {
            try
            {
                var workOrderClass = _modelConverter.ToClass(workOrder);

                await _collection.ReplaceOneAsync(doc => doc.Id == workOrderClass.Id, workOrderClass);
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

        public async Task DeleteAsync(Guid id, int version)
        {
            try
            {
                var findById = await _collection.FindAsync(doc => doc.Id == id);
                var user = findById.FirstOrDefault();
                if (user == null)
                {
                    throw Error.EntityNotFoundDb();
                }

                var result = await _collection.DeleteOneAsync(doc => doc.Id == id && doc.Version == version);
                if (result.DeletedCount == 0)
                {
                    throw Error.EntityVersionDb();
                }
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
