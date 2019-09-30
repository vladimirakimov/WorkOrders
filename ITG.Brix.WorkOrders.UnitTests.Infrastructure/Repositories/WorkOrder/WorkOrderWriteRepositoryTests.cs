using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Converters;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public class WorkOrderWriteRepositoryTests
    {
        [TestMethod]
        public void CtorShouldSucceed()
        {
#if DEBUG
            // Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            IPersistenceContext persistenceContext = new PersistenceContext(persistenceConfiguration);
            IModelConverter modelConverter = new Mock<IModelConverter>().Object;

            // Act
            Action ctor = () => { new WorkOrderWriteRepository(persistenceContext, modelConverter); };

            // Assert
            ctor.Should().NotThrow();
#endif
        }

        [TestMethod]
        public void CtorShouldFailWhenPersistenceContextNull()
        {
            // Arrange
            IPersistenceContext persistenceContext = null;
            IModelConverter modelConverter = new Mock<IModelConverter>().Object;

            // Act
            Action ctor = () => { new WorkOrderWriteRepository(persistenceContext, modelConverter); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void CtorShouldFailWhenModelConverterNull()
        {
            // Arrange
            var persistenceConfiguration = new PersistenceConfiguration("mongodb://localhost:C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==@localhost:10255/admin?ssl=true");
            IPersistenceContext persistenceContext = new PersistenceContext(persistenceConfiguration);
            IModelConverter modelConverter = null;

            // Act
            Action ctor = () => { new WorkOrderWriteRepository(persistenceContext, modelConverter); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
