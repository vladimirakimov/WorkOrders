using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Converters;
using ITG.Brix.WorkOrders.Infrastructure.Converters.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class WorkOrdersReadRepositoryTests
    {
        private IWorkOrderReadRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init("WorkOrders");
            IOdataProvider odataProvider = new OdataProvider();
            IModelConverter modelConverter = new ModelConverter(new TypeConverterProvider(), new DateTimeProvider());
            _repository = new WorkOrderReadRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)), odataProvider, modelConverter);
        }

        [TestMethod]
        public async Task GetByIdShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var userCreated = "Author";
            var order = new Order();

            RepositoryHelper.ForWorkOrder.CreateWorkOrder(id, userCreated, order);

            // Act
            var result = await _repository.GetAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.UserCreated.Should().Be(userCreated);
        }

        [TestMethod]
        public void GetByNonExistentIdShouldThrowEntityNotFoundDbException()
        {
            // Arrange
            var nonExistentWorkOrderId = Guid.NewGuid();

            // Act
            Func<Task> call = async () => { await _repository.GetAsync(nonExistentWorkOrderId); };

            // Assert
            call.Should().Throw<EntityNotFoundDbException>();
        }

        [TestMethod]
        public async Task ListShouldReturnAllRecords()
        {
            // Arrange
            var order = new Order();
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Ecc", order);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Ecc", order);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
        }

        [TestMethod]
        public async Task ListShouldReturnAllCreatedWorkOrdersRecords()
        {
            // Arrange
            var order = new Order();
            RepositoryHelper.ForWorkOrder.CreateStartedWorkOrder(Guid.NewGuid(), "Plato", order);
            RepositoryHelper.ForWorkOrder.CreateStartedWorkOrder(Guid.NewGuid(), "Ecc", order);
            RepositoryHelper.ForWorkOrder.CreateStartedWorkOrder(Guid.NewGuid(), "Ecc", order);

            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().HaveCount(3);
            result.Select(x => x.Operational.StartedOn).ToList().ForEach(x => x.Should().NotBeNull());
            result.Select(x => x.Operational.StartedOn).ToList().Should().AllBeOfType<DateOn>();
            result.Select(x => x.Operational.StartedOn).ToList().ForEach(x => x.Should().NotBeSameAs(new DateOn(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))));
            result.First().Operational.StartedOn.Should().NotBeNull();
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResult()
        {
            // Arrange
            var order = new Order();
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Ecc", order);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Ecc", order);

            var filter = "UserCreated eq 'Plato'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByCompoundFilter()
        {
            // Arrange
            var order1 = new Order()
            {
                Origin = new Origin()
                {
                    Source = "KBT"
                },
                Transport = new Transport()
                {
                    Type = "PTRI",
                    Ard = new Ard()
                    {
                        Reference1 = "EORDERTEST",
                        Reference2 = "KBUEN00010081",
                        Reference3 = "WVN1"
                    }
                },
                Operation = new Operation()
                {
                    Site = "LB1227",
                },

            };
            var order2 = new Order()
            {
                Origin = new Origin()
                {
                    Source = "KBT+T"
                },
                Transport = new Transport()
                {
                    Type = "CP",
                    Ard = new Ard()
                    {
                        Reference1 = "EORDERTESTREMARKS",
                        Reference2 = "KBUEN00010082",
                        Reference3 = "WVN2"
                    }
                },
                Operation = new Operation()
                {
                    Site = "LUITHAGEN"
                }
            };
            var order3 = new Order()
            {
                Origin = new Origin()
                {
                    Source = "KBT"
                },
                Transport = new Transport()
                {
                    Type = "PTRI",
                    Ard = new Ard()
                    {
                        Reference1 = "EORDERTEST",
                        Reference2 = "KBUEN00010081",
                        Reference3 = "WVN1"
                    }
                },
                Operation = new Operation()
                {
                    Site = "LB1227"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "(OrderOriginSource eq 'KBT' or OrderOriginSource eq 'KBT+T') and (OrderOperationSite eq 'LUITHAGEN') and (OrderTransportArdReference1 eq 'EORDERTEST' or OrderTransportArdReference1 eq 'EORDERTESTREMARKS')";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOriginSource()
        {
            // Arrange
            var order1 = new Order()
            {
                Origin = new Origin()
                {
                    Source = "KBT"
                }
            };
            var order2 = new Order()
            {
                Origin = new Origin()
                {
                    Source = "KBT+T"
                }
            };
            var order3 = new Order()
            {
                Origin = new Origin()
                {
                    Source = "KBT"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOriginSource eq 'KBT'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOperationSite()
        {
            // Arrange
            var order1 = new Order()
            {
                Operation = new Operation()
                {
                    Site = "LB1227"
                }
            };
            var order2 = new Order()
            {
                Operation = new Operation()
                {
                    Site = "LUITHAGEN"
                }
            };
            var order3 = new Order()
            {
                Operation = new Operation()
                {
                    Site = "LB1227"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOperationSite eq 'LB1227'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOperationName()
        {
            // Arrange
            var order1 = new Order()
            {
                Operation = new Operation()
                {
                    Name = "Unload into warehouse"
                }
            };
            var order2 = new Order()
            {
                Operation = new Operation()
                {
                    Name = "Unload into warehouse"
                }
            };
            var order3 = new Order()
            {
                Operation = new Operation()
                {
                    Name = "Load into warehouse"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOperationName eq 'Unload into warehouse'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOperationDockingZone()
        {
            // Arrange
            var order1 = new Order()
            {
                Operation = new Operation()
                {
                    DockingZone = "DockingZone 1"
                }
            };
            var order2 = new Order()
            {
                Operation = new Operation()
                {

                    DockingZone = "DockingZone 2"
                }
            };
            var order3 = new Order()
            {
                Operation = new Operation()
                {
                    DockingZone = "DockingZone 2"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOperationDockingZone eq 'DockingZone 2'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }


        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOperationOperationalDepartment()
        {
            // Arrange
            var order1 = new Order()
            {
                Operation = new Operation()
                {
                    OperationalDepartment = "DEP"
                }
            };
            var order2 = new Order()
            {
                Operation = new Operation()
                {
                    OperationalDepartment = "UDEP"
                }
            };
            var order3 = new Order()
            {
                Operation = new Operation()
                {
                    OperationalDepartment = "DEP"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOperationOperationalDepartment eq 'DEP'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOperationTypePlanning()
        {
            // Arrange
            var order1 = new Order()
            {
                Operation = new Operation()
                {
                    TypePlanning = "MAG"
                }
            };
            var order2 = new Order()
            {
                Operation = new Operation()
                {
                    TypePlanning = "MAG"
                }
            };
            var order3 = new Order()
            {
                Operation = new Operation()
                {
                    TypePlanning = "MAGG"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOperationTypePlanning eq 'MAG'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByCustomerCode()
        {
            // Arrange
            var order1 = new Order()
            {
                Customer = new Customer()
                {
                    Code = "DBPLASTICS"
                }
            };
            var order2 = new Order()
            {
                Customer = new Customer()
                {
                    Code = "EXXON"
                }
            };
            var order3 = new Order()
            {
                Customer = new Customer()
                {
                    Code = "DBPLASTICS"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderCustomerCode eq 'DBPLASTICS'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByNumber()
        {
            // Arrange
            var order1 = new Order()
            {
                Number = "Number1"
            };
            var order2 = new Order()
            {
                Number = "Number2"
            };
            var order3 = new Order()
            {
                Number = "Number3"
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderNumber eq 'Number2'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportLoadingPlace()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Loading = new Loading()
                    {
                        Place = "LoadingPlace1"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Loading = new Loading()
                    {
                        Place = "LoadingPlace2"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Loading = new Loading()
                    {
                        Place = "LoadingPlace2"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportLoadingPlace eq 'LoadingPlace2'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportLoadingReference()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Loading = new Loading()
                    {
                        Reference = "LoadingReference1"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Loading = new Loading()
                    {
                        Reference = "LoadingReference2"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Loading = new Loading()
                    {
                        Reference = "LoadingReference2"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportLoadingReference eq 'LoadingReference2'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }


        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportTruckLicensePlateTruck()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Truck = new Truck()
                    {
                        LicensePlateTruck = "TR"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Truck = new Truck()
                    {
                        LicensePlateTruck = "TT"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Truck = new Truck()
                    {
                        LicensePlateTruck = "TT"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportTruckLicensePlateTruck eq 'TT'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportTruckLicensePlateTrailer()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Truck = new Truck()
                    {
                        LicensePlateTrailer = "TR"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Truck = new Truck()
                    {
                        LicensePlateTrailer = "TT"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Truck = new Truck()
                    {
                        LicensePlateTrailer = "TT"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportTruckLicensePlateTrailer eq 'TT'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportContainerNumber()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        Number = "CN"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        Number = "CC"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        Number = "CC"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportContainerNumber eq 'CC'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportContainerLocation()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        Location = "ZONEA"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        Location = "ZONEB"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        Location = "ZONEB"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportContainerLocation eq 'ZONEB'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportContainerStackLocation()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        StackLocation = "StackZONEA"
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        StackLocation = "StackZONEB"
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Container = new Container()
                    {
                        StackLocation = "StackZONEB"
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportContainerStackLocation eq 'StackZONEB'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByOriginEntryNumber()
        {
            // Arrange
            var order1 = new Order()
            {
                Origin = new Origin()
                {
                    EntryNumber = "1000"
                }
            };
            var order2 = new Order()
            {
                Origin = new Origin()
                {
                    EntryNumber = "2000"
                }
            };
            var order3 = new Order()
            {
                Origin = new Origin()
                {
                    EntryNumber = "2000"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderOriginEntryNumber eq '2000'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByCustomerProductionSite()
        {
            // Arrange
            var order1 = new Order()
            {
                Customer = new Customer()
                {
                    ProductionSite = "PROD"
                }
            };
            var order2 = new Order()
            {
                Customer = new Customer()
                {
                    ProductionSite = "DEV"
                }
            };
            var order3 = new Order()
            {
                Customer = new Customer()
                {
                    ProductionSite = "PROD"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderCustomerProductionSite eq 'PROD'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByTransportType()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Type = "PTRI"
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Type = "CP"
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Type = "PTRI"
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportType eq 'PTRI'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnFilteredResultByDriverWait()
        {
            // Arrange
            var order1 = new Order()
            {
                Transport = new Transport()
                {
                    Driver = new Driver()
                    {
                        Wait = Wait.Yes
                    }
                }
            };
            var order2 = new Order()
            {
                Transport = new Transport()
                {
                    Driver = new Driver()
                    {
                        Wait = Wait.Undefined
                    }
                }
            };
            var order3 = new Order()
            {
                Transport = new Transport()
                {
                    Driver = new Driver()
                    {
                        Wait = Wait.No
                    }
                }
            };
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order1);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order2);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order3);

            var filter = "OrderTransportDriverWait eq 'Yes'";

            // Act
            var result = await _repository.ListAsync(filter, null, null);

            // Assert
            result.Should().HaveCount(1);
        }

        [TestMethod]
        public void ListShouldThrowFilterODataException()
        {
            // Arrange
            var filter = "unexistent eq 'KBT'";

            // Act
            Func<Task> call = async () => { await _repository.ListAsync(filter, null, null); };

            // Assert
            call.Should().Throw<FilterODataException>();
        }

        [TestMethod]
        public async Task ListShouldReturnCorrectResultWithLimitOnly()
        {
            // Arrange
            var order = new Order();
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order);
            RepositoryHelper.ForWorkOrder.CreateWorkOrder(Guid.NewGuid(), "Plato", order);

            int? limit = 2;

            // Act
            var result = await _repository.ListAsync(null, null, limit);

            // Assert
            result.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task ListShouldReturnEmptyResult()
        {
            // Act
            var result = await _repository.ListAsync(null, null, null);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
