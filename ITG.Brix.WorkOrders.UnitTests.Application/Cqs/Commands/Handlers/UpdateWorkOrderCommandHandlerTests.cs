using FluentAssertions;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Dtos;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Handlers;
using ITG.Brix.WorkOrders.Application.DataTypes;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Commands.Handlers
{
    [TestClass]
    public class UpdateWorkOrderCommandHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;
            var typeConverterProvider = new Mock<ITypeConverterProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);
            };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLogAsIsNull()
        {
            // Arrange
            ILogAs logAs = null;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;
            var typeConverterProvider = new Mock<ITypeConverterProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWorkOrderWriteRepositoryIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            IWorkOrderWriteRepository workOrderWriteRepository = null;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;
            var typeConverterProvider = new Mock<ITypeConverterProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWorkOrderReadRepositoryIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            IWorkOrderReadRepository workOrderReadRepository = null;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;
            var typeConverterProvider = new Mock<ITypeConverterProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenVersionProviderIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            IVersionProvider versionProvider = null;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;
            var typeConverterProvider = new Mock<ITypeConverterProvider>().Object;

            // Act
            Action ctor = () =>
            {
                new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenTypeConverterProviderIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var workOrderWriteRepository = new Mock<IWorkOrderWriteRepository>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var versionProvider = new Mock<IVersionProvider>().Object;
            var dateTimeProvider = new Mock<IDateTimeProvider>().Object;
            ITypeConverterProvider typeConverterProvider = null;

            // Act
            Action ctor = () =>
            {
                new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);
            };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var id = Guid.NewGuid();
            var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
            var stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());
            var isEditable = true;
            Order order = new Order();
            Operational operational = new Operational(Status.Busy);
            string userCreated = "any";
            DateTime createdOn = DateTime.UtcNow;

            var version = 1;

            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var workOrderWriteRepositoryMock = new Mock<IWorkOrderWriteRepository>();
            workOrderWriteRepositoryMock.Setup(x => x.UpdateAsync(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
            var workOrderWriteRepository = workOrderWriteRepositoryMock.Object;

            var workOrderReadRepositoryMock = new Mock<IWorkOrderReadRepository>();
            workOrderReadRepositoryMock.Setup(x => x.GetAsync(id)).Returns(Task.FromResult(new WorkOrder(id, isEditable, order, operational, userCreated, startCreatedOn) { Version = 1 }));
            var workOrderReadRepository = workOrderReadRepositoryMock.Object;

            var versionProviderMock = new Mock<IVersionProvider>();
            versionProviderMock.Setup(x => x.Generate()).Returns(version);
            var versionProvider = versionProviderMock.Object;

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.Parse(It.IsAny<string>())).Returns(DateTime.UtcNow);
            var dateTimeProvider = dateTimeProviderMock.Object;

            var typeConverterProviderMock = new Mock<ITypeConverterProvider>();
            typeConverterProviderMock.Setup(x => x.ToFloat(It.IsAny<string>())).Returns(12);
            var typeConverterProvider = typeConverterProviderMock.Object;


            var command = new UpdateWorkOrderCommand(
                                id,
                                new Optional<string>("AnyOperant"),
                                new Optional<string>(Status.Busy.Name),
                                new Optional<string>(DateTime.UtcNow.ToString()),
                                new Optional<IEnumerable<HandledUnitDto>>(),
                                new Optional<IEnumerable<RemarkDto>>(),
                                new Optional<IEnumerable<PictureDto>>(),
                                new Optional<IEnumerable<InputDto>>(),
                                1
                          );


            var handler = new UpdateWorkOrderCommandHandler(logAs, workOrderReadRepository, workOrderWriteRepository, versionProvider, dateTimeProvider, typeConverterProvider);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result));
        }

        //[TestMethod]
        //public async Task HandleShouldReturnFailWhenNotFound()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var isEditable = true;

        //    string userCreated = "any";
        //    DateTime createdOn = DateTime.Now;

        //    var version = 1;


        //    var workOrderWriteRepositoryMock = new Mock<IWorkOrderWriteRepository>();
        //    workOrderWriteRepositoryMock.Setup(x => x.Update(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
        //    var workOrderWriteRepository = workOrderWriteRepositoryMock.Object;

        //    var workOrderReadRepositoryMock = new Mock<IWorkOrderReadRepository>();
        //    workOrderReadRepositoryMock.Setup(x => x.Get(id)).Throws<EntityNotFoundDbException>();
        //    var workOrderReadRepository = workOrderReadRepositoryMock.Object;

        //    var versionProviderMock = new Mock<IVersionProvider>();
        //    versionProviderMock.Setup(x => x.Generate()).Returns(version);
        //    var versionProvider = versionProviderMock.Object;

        //    var command = new UpdateWorkOrderCommand(id, isEditable, userCreated, createdOn.ToString(), new OrderDto(), new OperationalDto(), version);


        //    var handler = new UpdateWorkOrderCommandHandler(workOrderReadRepository, workOrderWriteRepository, versionProvider);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);



        //    // Assert
        //    result.IsFailure.Should().BeTrue();
        //    result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotFound.Name &&
        //                                              x.Target == "id");
        //}

        //[TestMethod]
        //public async Task HandleShouldReturnFailWhenOutdatedVersion()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
        //    var stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());
        //    var isEditable = true;
        //    Order order = new Order();
        //    Operational operational = new Operational("any", "any", "any", startCreatedOn, stopCreatedOn);

        //    string userCreated = "any";
        //    DateTime createdOn = DateTime.Now;

        //    var version = 1;


        //    var workOrderWriteRepositoryMock = new Mock<IWorkOrderWriteRepository>();
        //    workOrderWriteRepositoryMock.Setup(x => x.Update(It.IsAny<WorkOrder>())).Returns(Task.CompletedTask);
        //    var workOrderWriteRepository = workOrderWriteRepositoryMock.Object;

        //    var workOrderReadRepositoryMock = new Mock<IWorkOrderReadRepository>();
        //    workOrderReadRepositoryMock.Setup(x => x.Get(id)).Returns(Task.FromResult(new WorkOrder(id, isEditable, order, operational, userCreated, startCreatedOn) { Version = 2 }));
        //    var workOrderReadRepository = workOrderReadRepositoryMock.Object;

        //    var versionProviderMock = new Mock<IVersionProvider>();
        //    versionProviderMock.Setup(x => x.Generate()).Returns(version);
        //    var versionProvider = versionProviderMock.Object;

        //    var command = new UpdateWorkOrderCommand(id, isEditable, userCreated, createdOn.ToString(), new OrderDto(), new OperationalDto(), version);

        //    var handler = new UpdateWorkOrderCommandHandler(workOrderReadRepository, workOrderWriteRepository, versionProvider);

        //    // Act
        //    var result = await handler.Handle(command, CancellationToken.None);

        //    // Assert
        //    result.IsFailure.Should().BeTrue();
        //    result.Failures.Should().OnlyContain(x => x.Code == HandlerFaultCode.NotMet.Name &&
        //                                              x.Message == HandlerFailures.NotMet &&
        //                                              x.Target == "version");
        //}
    }
}
