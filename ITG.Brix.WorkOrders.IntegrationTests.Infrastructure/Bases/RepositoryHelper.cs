using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.Converters.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using System;
using System.Collections.Generic;

namespace ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Bases
{
    public static class RepositoryHelper
    {
        public static class ForWorkOrder
        {
            public static WorkOrder CreateStartedWorkOrder(Guid id, string userCreated, Order order)
            {
                // prepare
                var odataProvider = new OdataProvider();
                var modelConverter = new ModelConverter(new TypeConverterProvider(), new DateTimeProvider());
                var writeRepository = new WorkOrderWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), modelConverter);
                var readRepository = new WorkOrderReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, modelConverter);

                // create
                var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());

                var operational = new Operational(Status.Open);
                operational.SetStartedOn(new DateOn(DateTime.UtcNow));
                var workOrder = new WorkOrder(id, true, order, operational, userCreated, startCreatedOn);

                writeRepository.CreateAsync(workOrder).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static WorkOrder CreateWorkOrder(Guid id, string userCreated, Order order)
            {
                // prepare
                var odataProvider = new OdataProvider();
                var modelConverter = new ModelConverter(new TypeConverterProvider(), new DateTimeProvider());
                var writeRepository = new WorkOrderWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), modelConverter);
                var readRepository = new WorkOrderReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, modelConverter);

                // create
                var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());

                var operational = new Operational(Status.Open);
                var workOrder = new WorkOrder(id, true, order, operational, userCreated, startCreatedOn);

                writeRepository.CreateAsync(workOrder).GetAwaiter().GetResult();

                // result
                var result = readRepository.GetAsync(id).Result;

                return result;
            }

            public static IEnumerable<WorkOrder> GetWorkOrders()
            {
                var odataProvider = new OdataProvider();
                var modelConverter = new ModelConverter(new TypeConverterProvider(), new DateTimeProvider());
                var repository = new WorkOrderReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, modelConverter);
                var result = repository.ListAsync(null, null, null).Result;

                return result;
            }
        }
    }
}
