using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class PlatoOrderWriteRepositoryTests
    {
        private IPlatoOrderWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init("PlatoOrders");
            _repository = new PlatoOrderWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var platoOrder = new PlatoOrder(id, "anyorderXml");

            // Act
            await _repository.CreateAsync(platoOrder);
        }
    }
}
