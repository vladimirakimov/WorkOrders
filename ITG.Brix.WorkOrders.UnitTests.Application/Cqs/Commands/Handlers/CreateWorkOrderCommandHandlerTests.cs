using FluentAssertions;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class CreateWorkOrderCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;

            // Act
            Action ctor = () => { new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLogAsIsNull()
        {
            // Arrange
            ILogAs logAs = null;
            var workOrderRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;

            // Act
            Action ctor = () => { new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWorkOrderRepositoryIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            IWorkOrderWriteRepository workOrderRepository = null;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;

            // Act
            Action ctor = () => { new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenIdentifierProviderIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderRepository = new Mock<IWorkOrderWriteRepository>().Object;
            IIdentifierProvider identifierProvider = null;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;

            // Act
            Action ctor = () => { new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var identifierProvider = new Mock<IIdentifierProvider>().Object;
            IVersionProvider versionProvider = null;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;

            // Act
            Action ctor = () => { new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }


        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var userCreated = "anyUserCreated";
            var operation = "TestOperation";
            var operationalDepartment = "any";
            var site = "any";


            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
            var workOrderRepository = workOrderRepositoryMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(id);
            var identifierProvider = identifierProviderMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(DateTime.UtcNow);
            var dateTimeProvider = dateTimeProviderMock.Object;

            var command = new CreateWorkOrderCommand(userCreated, site, operation, operationalDepartment);

            var handler = new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<Guid>));
        }



        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var userCreated = "anyUserCreated";
            var operation = "TestOperation";
            var operationalDepartment = "any";
            var site = "any";

            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderRepositoryMock.Setup(x => x.CreateAsync(It.IsAny<WorkOrder>())).Throws<SomeDatabaseSpecificException>();
            var workOrderRepository = workOrderRepositoryMock.Object;

            var identifierProviderMock = new Mock<IIdentifierProvider>();
            identifierProviderMock.Setup(x => x.Generate()).Returns(id);
            var identifierProvider = identifierProviderMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(DateTime.UtcNow);
            var dateTimeProvider = dateTimeProviderMock.Object;

            var command = new CreateWorkOrderCommand(userCreated, site, operation, operationalDepartment);

            var handler = new CreateWorkOrderCommandHandler(logAs, workOrderRepository, identifierProvider, versionProvider, dateTimeProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.CreateWorkOrderFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
