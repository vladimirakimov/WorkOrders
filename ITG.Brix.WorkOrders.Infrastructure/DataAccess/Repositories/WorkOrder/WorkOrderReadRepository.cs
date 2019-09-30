using ITG.Brix.Diagnostics.Guards;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Constants;
using ITG.Brix.WorkOrders.Infrastructure.Converters;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassModels;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations;
using ITG.Brix.WorkOrders.Infrastructure.Diagnostics;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories
{
    public class WorkOrderReadRepository : IWorkOrderReadRepository
    {
        private readonly IOdataProvider _odataProvider;
        private readonly IModelConverter _modelConverter;

        private readonly IMongoCollection<WorkOrderClass> _collection;

        public WorkOrderReadRepository(IPersistenceContext persistenceContext,
                                       IOdataProvider odataProvider,
                                       IModelConverter modelConverter)
        {
            Guard.On(persistenceContext, Error.ArgumentNull(nameof(persistenceContext))).AgainstNull();
            Guard.On(odataProvider, Error.ArgumentNull(nameof(odataProvider))).AgainstNull();
            Guard.On(modelConverter, Error.ArgumentNull(nameof(modelConverter))).AgainstNull();

            _odataProvider = odataProvider;
            _modelConverter = modelConverter;
            _collection = persistenceContext.Database.GetCollection<WorkOrderClass>(Consts.Collections.WorkOrders);
        }

        public async Task<WorkOrder> GetAsync(Guid id)
        {
            try
            {
                var findById = await _collection.FindAsync(doc => doc.Id == id);
                var workOrderClass = findById.FirstOrDefault();
                if (workOrderClass == null)
                {
                    throw Error.EntityNotFoundDb();
                }
                var workOrder = _modelConverter.ToDomain(workOrderClass);

                return workOrder;
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

        public async Task<IList<WorkOrder>> ListAsync(string filter, int? skip, int? limit)
        {
            var filterDefinition = _odataProvider.GetFilterPredicate(filter);

            IFindFluent<WorkOrderClass, WorkOrderClass> fluent = null;
            if (filterDefinition == null)
            {
                var filterEmpty = Builders<WorkOrderClass>.Filter.Empty;
                fluent = _collection.Find(filterEmpty);
            }
            else
            {
                fluent = _collection.Find(filterDefinition);
            }

            fluent = fluent.Skip(skip).Limit(limit);

            var workOrderClasses = await fluent.ToListAsync();

            var result = new List<WorkOrder>();
            foreach (var workOrderClass in workOrderClasses)
            {
                var workOrder = _modelConverter.ToDomain(workOrderClass);
                result.Add(workOrder);
            }

            return result;
        }
    }
}
