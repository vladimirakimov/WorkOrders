using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Converters;
using ITG.Brix.WorkOrders.Infrastructure.Converters.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class WorkOrdersWriteRepositoryTests
    {
        private IWorkOrderWriteRepository _repository;
        private IDateTimeProvider _dateTimeProvider;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init("WorkOrders");
            IModelConverter modelConverter = new ModelConverter(new TypeConverterProvider(), new DateTimeProvider());
            _repository = new WorkOrderWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), modelConverter);
            _dateTimeProvider = new DateTimeProvider();
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
            var stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());
            var order = new Order();

            var operational = new Operational(Status.Open);
            var workOrder = new WorkOrder(Guid.NewGuid(), true, order, operational, "anyUser", startCreatedOn);

            // Act
            await _repository.CreateAsync(workOrder);

            // Assert
            var data = RepositoryHelper.ForWorkOrder.GetWorkOrders();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.UserCreated.Should().Be("anyUser");
        }

        [TestMethod]
        public async Task DeleteShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var order = new Order();
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(id, "any", order);

            // Act
            await _repository.DeleteAsync(id, 0);

            // Assert
            var data = RepositoryHelper.ForWorkOrder.GetWorkOrders();
            data.Should().HaveCount(0);
        }

        [TestMethod]
        public async Task UpdateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
            var stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());
            var order = new Order();

            var workOrderToUpdate = RepositoryHelper.ForWorkOrder.CreateWorkOrder(id, "anyUser", order);
            var operationalToUpdate = workOrderToUpdate.Operational;
            operationalToUpdate.SetOperant("Operant");
            operationalToUpdate.SetStartedOn(new DateOn(_dateTimeProvider.Parse("2019-04-19T12:15:57Z").Value));

            // Act
            await _repository.UpdateAsync(workOrderToUpdate);

            // Assert
            var data = RepositoryHelper.ForWorkOrder.GetWorkOrders();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.UserCreated.Should().Be("anyUser");
            result.Operational.Operant.Should().Be("Operant");
            result.Operational.StartedOn.Should().NotBeNull();
        }

        [TestMethod]
        public async Task UpdateOperationalHandledUnitsShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var order = new Order();
            var unitId = Guid.NewGuid();

            var unit = new Unit(unitId, UnitType.Single);
            var location = new Location(
                new Warehouse(new Label("Warehouse")),
                new Gate(new Label("Gate")),
                new Row(new Label("Row")),
                new Position(new Label("Position"))
            );
            unit.SetLocation(location);
            unit.SetGroup(new Group(
                            key: "groupKey",
                            weightNet: new Weight(12),
                            weightGross: new Weight(14)
                            )
            );
            unit.SetMixed(new Mixed(key: null, palletNumber: null));
            unit.SetUnits(new Units(1));
            unit.SetIsPartial(false);
            unit.SetQuantity(new Quantity(55));
            unit.SetWeightNet(new Weight(12));
            unit.SetWeightGross(new Weight(14));
            unit.SetPalletNumber("PalletNumber");
            unit.SetSsccNumber("SsccNumber");


            order.Units = new List<Unit>() { unit };
            var workOrderToUpdate = RepositoryHelper.ForWorkOrder.CreateWorkOrder(id, "anyUser", order);
            var operationalToUpdate = workOrderToUpdate.Operational;

            operationalToUpdate.ClearHandledUnits();


            var handledUnitId = Guid.NewGuid();
            var operantId = Guid.NewGuid();
            var operantLogin = new Login("OperantLogin");
            var sourceUnitId = unit.Id;
            var locationWarehouse = "Warehouse";
            var locationGate = "Gate";
            var locationRow = "Row";
            var locationPosition = "Position";
            var units = new Units(1);
            var isPartial = false;
            var isMixed = false;
            var quantity = new Quantity(55);
            var weightNet = new Weight(13);
            var weightGross = new Weight(15);
            var palletNumber = "PalletNumber";
            var ssccNumber = "SsccNumber";

            var operant = new Operant(operantId, operantLogin);
            var sourceUnit = workOrderToUpdate.Order.Units.First(x => x.Id == sourceUnitId);
            var handledOn = new HandledOn(DateTime.UtcNow);
            var handledLocation = new Location(
                                new Warehouse(new Label(locationWarehouse)),
                                new Gate(new Label(locationGate)),
                                new Row(new Label(locationRow)),
                                new Position(new Label(locationPosition))
                           );
            var type = sourceUnit.Type;




            var handledUnit = new HandledUnit(handledUnitId, sourceUnit);
            handledUnit.SetOperant(operant);
            handledUnit.SetHandledOn(handledOn);
            handledUnit.SetLocation(handledLocation);
            handledUnit.SetType(type);

            handledUnit.SetUnits(units);
            handledUnit.SetIsPartial(isPartial);
            handledUnit.SetIsMixed(isMixed);
            handledUnit.SetQuantity(quantity);
            handledUnit.SetWeightNet(weightNet);
            handledUnit.SetWeightGross(weightGross);

            handledUnit.SetPalletNumber(palletNumber);
            handledUnit.SetSsccNumber(ssccNumber);

            operationalToUpdate.AddHandledUnit(handledUnit);

            operationalToUpdate.SetOperant("Operant");
            operationalToUpdate.SetStartedOn(new DateOn(_dateTimeProvider.Parse("2019-04-19T12:15:57Z").Value));

            // Act
            await _repository.UpdateAsync(workOrderToUpdate);

            // Assert
            var data = RepositoryHelper.ForWorkOrder.GetWorkOrders();
            data.Should().HaveCount(1);
            var result = data.First();
            result.Should().NotBeNull();
            result.Operational.HandledUnits.Count.Should().Be(1);
        }
    }
}
