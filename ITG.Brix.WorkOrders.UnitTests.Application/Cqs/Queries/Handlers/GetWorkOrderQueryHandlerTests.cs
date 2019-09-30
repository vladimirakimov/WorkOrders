using AutoMapper;
using FluentAssertions;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Handlers;
using ITG.Brix.WorkOrders.Application.Services;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrators;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class GetWorkOrderQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var mapper = new Mock<IMapper>().Object;
            var orchestrator = new Mock<IOrchestrator>().Object;
            var workOrderReadrepository = new Mock<IWorkOrderReadRepository>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var platoOrderProvider = new Mock<IPlatoOrderProvider>().Object;
            var domainConverter = new Mock<IDomainConverter>().Object;

            // Act
            Action ctor = () => { new GetWorkOrderQueryHandler(logAs, mapper, orchestrator, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, domainConverter); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLogAsIsNull()
        {
            // Arrange
            ILogAs logAs = null;
            IMapper mapper = new Mock<IMapper>().Object;
            var orchestrator = new Mock<IOrchestrator>().Object;
            var workOrderReadrepository = new Mock<IWorkOrderReadRepository>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var platoOrderProvider = new Mock<IPlatoOrderProvider>().Object;
            var domainConverter = new Mock<IDomainConverter>().Object;

            // Act
            Action ctor = () => { new GetWorkOrderQueryHandler(logAs, mapper, orchestrator, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, domainConverter); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            IMapper mapper = null;
            var orchestrator = new Mock<IOrchestrator>().Object;
            var workOrderReadrepository = new Mock<IWorkOrderReadRepository>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var platoOrderProvider = new Mock<IPlatoOrderProvider>().Object;
            var domainConverter = new Mock<IDomainConverter>().Object;

            // Act
            Action ctor = () => { new GetWorkOrderQueryHandler(logAs, mapper, orchestrator, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, domainConverter); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenOrchestratorIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            IMapper mapper = new Mock<IMapper>().Object;
            IOrchestrator orchestrator = null;
            var workOrderReadrepository = new Mock<IWorkOrderReadRepository>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var platoOrderProvider = new Mock<IPlatoOrderProvider>().Object;
            var domainConverter = new Mock<IDomainConverter>().Object;

            // Act
            Action ctor = () => { new GetWorkOrderQueryHandler(logAs, mapper, orchestrator, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, domainConverter); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenReadRepoIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var mapper = new Mock<IMapper>().Object;
            var orchestrator = new Mock<IOrchestrator>().Object;
            IWorkOrderReadRepository workOrderReadrepository = null;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var platoOrderProvider = new Mock<IPlatoOrderProvider>().Object;
            var domainConverter = new Mock<IDomainConverter>().Object;

            // Act
            Action ctor = () => { new GetWorkOrderQueryHandler(logAs, mapper, orchestrator, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, domainConverter); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }


        //[TestMethod]
        //public async Task HandleShouldReturnOk()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
        //    var stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());

        //    var mapperMock = new Mock<IMapper>();
        //    mapperMock.Setup(x => x.Map<WorkOrderModel>(It.IsAny<object>())).Returns(new WorkOrderModel());
        //    var mapper = mapperMock.Object;

        //    var biztalkRestApiMock = new Mock<IBiztalkRestApi>();
        //    biztalkRestApiMock.Setup(x => x.GetOrder(It.IsAny<object>())).Returns(Task.FromResult(string.Empty));
        //    var biztalkRestApi = biztalkRestApiMock.Object;

        //    var workOrderReadrepositoryMock = new Mock<IWorkOrderReadRepository>();
        //    workOrderReadrepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new WorkOrder(id, true, new Order(), new Operational("any", "any", "any", startCreatedOn, stopCreatedOn), "any", startCreatedOn)));
        //    var workOrderReadrepository = workOrderReadrepositoryMock.Object;

        //    var workOrderWriteRepositoryMock = new Mock<IWorkOrderWriteRepository>();
        //    workOrderWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
        //    var workOrderWriteRepository = workOrderWriteRepositoryMock.Object;

        //    var platoOrderProviderMock = new Mock<IPlatoOrderProvider>();
        //    platoOrderProviderMock.Setup(x => x.GetPlatoOrderFull(It.IsAny<string>())).Returns(new PlatoOrderFull());
        //    var platoOrderProvider = platoOrderProviderMock.Object;

        //    var dateTimeProviderMock = new Mock<IDateTimeProvider>();
        //    dateTimeProviderMock.Setup(x => x.Parse(It.IsAny<string>())).Returns((DateTime?)null);
        //    var dateTimeProvider = dateTimeProviderMock.Object;

        //    var query = new GetWorkOrderQuery(id);

        //    var handler = new GetWorkOrderQueryHandler(mapper, biztalkRestApi, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, dateTimeProvider);

        //    // Act
        //    var result = await handler.Handle(query, CancellationToken.None);

        //    // Assert
        //    result.IsFailure.Should().BeFalse();
        //    result.Should().BeOfType(typeof(Result<WorkOrderModel>));
        //}

        /*
    [TestMethod]
    [Ignore("Biztalk: supress-try")]
    public async Task HandleShouldReturnFailWhenNotFound()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<WorkOrderModel>(It.IsAny<object>())).Returns(new WorkOrderModel());
        var mapper = mapperMock.Object;

        var biztalkRestApiMock = new Mock<IBiztalkRestApi>();
        biztalkRestApiMock.Setup(x => x.GetOrder(It.IsAny<string>())).Returns(Task.FromResult(string.Empty));
        var biztalkRestApi = biztalkRestApiMock.Object;

        var workOrderReadrepositoryMock = new Mock<IWorkOrderReadRepository>();
        workOrderReadrepositoryMock.Setup(x => x.GetAsync(id)).Throws<EntityNotFoundDbException>();
        var workOrderReadrepository = workOrderReadrepositoryMock.Object;

        var workOrderWriteRepositoryMock = new Mock<IWorkOrderWriteRepository>();
        workOrderWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
        var workOrderWriteRepository = workOrderWriteRepositoryMock.Object;

        var platoOrderProviderMock = new Mock<IPlatoOrderProvider>();
        platoOrderProviderMock.Setup(x => x.GetPlatoOrderFull(It.IsAny<string>())).Returns(new PlatoOrderFull());
        var platoOrderProvider = platoOrderProviderMock.Object;

        var dateTimeProviderMock = new Mock<IDateTimeProvider>();
        dateTimeProviderMock.Setup(x => x.Parse(It.IsAny<string>())).Returns((DateTime?)null);
        var dateTimeProvider = dateTimeProviderMock.Object;

        var query = new GetWorkOrderQuery(id);

        var handler = new GetWorkOrderQueryHandler(mapper, biztalkRestApi, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, dateTimeProvider);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name &&
                                                  x.Target == "id");
    }

    [TestMethod]
    [Ignore("Biztalk: supress-try")]
    public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
    {
        // Arrange
        var id = Guid.NewGuid();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(x => x.Map<WorkOrderModel>(It.IsAny<object>())).Returns(new WorkOrderModel());
        var mapper = mapperMock.Object;

        var biztalkRestApiMock = new Mock<IBiztalkRestApi>();
        biztalkRestApiMock.Setup(x => x.GetOrder(It.IsAny<string>())).Returns(Task.FromResult(string.Empty));
        var biztalkRestApi = biztalkRestApiMock.Object;

        var workOrderReadrepositoryMock = new Mock<IWorkOrderReadRepository>();
        workOrderReadrepositoryMock.Setup(x => x.GetAsync(id)).Throws<SomeDatabaseSpecificException>();
        var workOrderReadrepository = workOrderReadrepositoryMock.Object;

        var workOrderWriteRepositoryMock = new Mock<IWorkOrderWriteRepository>();
        workOrderWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
        var workOrderWriteRepository = workOrderWriteRepositoryMock.Object;

        var platoOrderProviderMock = new Mock<IPlatoOrderProvider>();
        platoOrderProviderMock.Setup(x => x.GetPlatoOrderFull(It.IsAny<string>())).Returns(new PlatoOrderFull());
        var platoOrderProvider = platoOrderProviderMock.Object;

        var dateTimeProviderMock = new Mock<IDateTimeProvider>();
        dateTimeProviderMock.Setup(x => x.Parse(It.IsAny<string>())).Returns((DateTime?)null);
        var dateTimeProvider = dateTimeProviderMock.Object;

        var query = new GetWorkOrderQuery(id);

        var handler = new GetWorkOrderQueryHandler(mapper, biztalkRestApi, workOrderReadrepository, workOrderWriteRepository, platoOrderProvider, dateTimeProvider);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.GetWorkOrderFailure);
    }

    */


        public class SomeDatabaseSpecificException : Exception { }
    }
}
