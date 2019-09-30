using AutoMapper;
using FluentAssertions;
using ITG.Brix.Diagnostics.Logging.Abstractions;
using ITG.Brix.WorkOrders.Application.Bases;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Handlers;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.Application.Resources;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Queries.Handlers
{
    [TestClass]
    public class ListWorkOrderQueryHandlerTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var mapper = new Mock<IMapper>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var filterProvider = new Mock<IFilterProvider>().Object;

            // Act
            Action ctor = () => { new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadRepository, filterProvider); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenLogAsIsNull()
        {
            // Arrange
            ILogAs logAs = null;
            var mapper = new Mock<IMapper>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var filterProvider = new Mock<IFilterProvider>().Object;

            // Act
            Action ctor = () => { new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadRepository, filterProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(logAs)}*");
        }

        [TestMethod]
        public void ConstructorShouldFailWhenMapperIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            IMapper mapper = null;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            var filterProvider = new Mock<IFilterProvider>().Object;

            // Act
            Action ctor = () => { new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadRepository, filterProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(mapper)}*");
        }

        [TestMethod]
        public void ConstructorShouldFailWhenWorkOrderReadRepositoryIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var mapper = new Mock<IMapper>().Object;
            IWorkOrderReadRepository workOrderReadRepository = null;
            var filterProvider = new Mock<IFilterProvider>().Object;

            // Act
            Action ctor = () => { new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadRepository, filterProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(workOrderReadRepository)}*");
        }

        [TestMethod]
        public void ConstructorShouldFailWhenFilterProviderIsNull()
        {
            // Arrange
            var logAs = new Mock<ILogAs>().Object;
            var mapper = new Mock<IMapper>().Object;
            var workOrderReadRepository = new Mock<IWorkOrderReadRepository>().Object;
            IFilterProvider filterProvider = null;

            // Act
            Action ctor = () => { new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadRepository, filterProvider); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>().WithMessage($"*{nameof(filterProvider)}*");
        }

        [TestMethod]
        public async Task HandleShouldReturnOk()
        {
            // Arrange
            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<WorkOrderModel>>(It.IsAny<object>())).Returns(new List<WorkOrderModel>());
            var mapper = mapperMock.Object;

            var workOrderReadrepositoryMock = new Mock<IWorkOrderReadRepository>();
            workOrderReadrepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Returns(Task.FromResult(new List<WorkOrder>() as IList<WorkOrder>));
            var workOrderReadrepository = workOrderReadrepositoryMock.Object;

            var filterProviderMock = new Mock<IFilterProvider>();
            filterProviderMock.Setup(x => x.Replace(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>())).Returns("filteredString");
            var filterProvider = filterProviderMock.Object;

            var query = new ListWorkOrderQuery(null, null, null);

            var handler = new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadrepository, filterProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeFalse();
            result.Should().BeOfType(typeof(Result<WorkOrdersModel>));
        }

        [TestMethod]
        public async Task HandleShouldReturnFailWhenDatabaseSpecificErrorOccurs()
        {
            // Arrange
            var logAsMock = new Mock<ILogAs>();
            logAsMock.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>()));
            var logAs = logAsMock.Object;

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<IEnumerable<WorkOrderModel>>(It.IsAny<object>())).Returns(new List<WorkOrderModel>());
            var mapper = mapperMock.Object;

            var workOrderReadrepositoryMock = new Mock<IWorkOrderReadRepository>();
            workOrderReadrepositoryMock.Setup(x => x.ListAsync(It.IsAny<string>(), It.IsAny<int?>(), It.IsAny<int?>())).Throws<SomeDatabaseSpecificException>();
            var workOrderReadrepository = workOrderReadrepositoryMock.Object;

            var filterProviderMock = new Mock<IFilterProvider>();
            filterProviderMock.Setup(x => x.Replace(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>())).Returns("filteredString");
            var filterProvider = filterProviderMock.Object;

            var query = new ListWorkOrderQuery(null, null, null);

            var handler = new ListWorkOrderQueryHandler(logAs, mapper, workOrderReadrepository, filterProvider);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);


            // Assert
            result.IsFailure.Should().BeTrue();
            result.Failures.Should().OnlyContain(x => x.Message == CustomFailures.ListWorkOrderFailure);
        }

        public class SomeDatabaseSpecificException : Exception { }
    }
}
