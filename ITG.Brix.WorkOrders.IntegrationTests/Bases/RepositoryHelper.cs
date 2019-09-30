using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Converters.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System;

namespace ITG.Brix.WorkOrders.IntegrationTests.Bases
{
    public static class RepositoryHelper
    {
        public static WorkOrder CreateWorkOrder(Guid id, Order order)
        {
            // prepare
            var writeRepository = new WorkOrderWriteRepository(new PersistenceContext(new PersistenceConfiguration(DatabaseHelper.ConnectionString)), new ModelConverter(new TypeConverterProvider(), new DateTimeProvider()));
            var readRepository = new WorkOrderReadRepository(new PersistenceContext(new PersistenceConfiguration(DatabaseHelper.ConnectionString)), new OdataProvider(), new ModelConverter(new TypeConverterProvider(), new DateTimeProvider()));

            // create
            var entity = new WorkOrder(id);
            entity.CreatedOn = new CreatedOn(DateTime.UtcNow);
            entity.UserCreated = "UserCreated";
            entity.IsEditable = false;
            entity.Order = order;
            entity.Operational = new Operational(Status.Open);


            writeRepository.CreateAsync(entity).GetAwaiter().GetResult();


            // result
            var result = readRepository.GetAsync(id).Result;

            return result;
        }
    }
}
