using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Repositories;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Configurations.Impl;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.Repositories;
using ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    [TestCategory("Integration")]
    public class ConceptWriteRepositoryTests
    {
        private IConceptWriteRepository _repository;

        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            RepositoryTestsHelper.Init("Concepts");
            _repository = new ConceptWriteRepository(new PersistenceContext(new PersistenceConfiguration(RepositoryTestsHelper.ConnectionString)));
        }

        [TestMethod]
        public async Task CreateShouldSucceed()
        {
            // Arrange
            var concept = new Concept() { CreatedOn = 33 };

            // Act
            await _repository.CreateAsync(concept);

            // Assert

        }

    }
}
