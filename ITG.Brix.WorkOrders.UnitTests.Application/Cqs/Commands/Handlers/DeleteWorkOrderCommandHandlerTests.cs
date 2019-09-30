using FluentAssertions;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class DeleteWorkOrderCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteWorkOrderCommandHandler(logAs, workOrderWriteRepository); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLogAsIsNull()
        {
            // Arrange
            ILogAs logAs = null;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;

            // Act
            Action ctor = () => { new DeleteWorkOrderCommandHandler(logAs, workOrderWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWorkOrderWriteRepositoryIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            IWorkOrderWriteRepository workOrderWriteRepository = null;

            // Act
            Action ctor = () => { new DeleteWorkOrderCommandHandler(logAs, workOrderWriteRepository); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Returns(Task.CompletedTask);
            var workOrderRepository = workOrderRepositoryMock.Object;

            var command = new DeleteWorkOrderCommand(id, version);

            var handler = new DeleteWorkOrderCommandHandler(logAs, workOrderRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenNotFound()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Throws<EntityNotFoundDbException>();
            var workOrderRepository = workOrderRepositoryMock.Object;

            var command = new DeleteWorkOrderCommand(id, version);

            var handler = new DeleteWorkOrderCommandHandler(logAs, workOrderRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenOutdatedVersion()
        {
            // Arrange
            var id = Guid.NewGuid();
            var version = 1;

            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Throws<EntityVersionDbException>();
            var workOrderRepository = workOrderRepositoryMock.Object;

            var command = new DeleteWorkOrderCommand(id, version);

            var handler = new DeleteWorkOrderCommandHandler(logAs, workOrderRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotMet.Name &&
                                                      x.Message == HandlerFailures.NotMet &&
                                                      x.Target == "version");
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            var id = Guid.NewGuid();
            var version = 1;

            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderRepositoryMock.Setup(x => x.DeleteAsync(id, version)).Throws<SomeDatabaseSpecificException>();
            var workOrderRepository = workOrderRepositoryMock.Object;

            var command = new DeleteWorkOrderCommand(id, version);

            var handler = new DeleteWorkOrderCommandHandler(logAs, workOrderRepository);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.DeleteWorkOrderFailure);
        }


        public class SomeDatabaseSpecificException : Exception { }
    }
}
