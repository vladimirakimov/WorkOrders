using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class OdataProviderTests
    {
        [DataTestMethod]
        [DataRow("UserCreated eq 'plato'")]
        public void GetFilterPredicateShouldSucceed(string filter)
        {
            // Arrange
            IOdataProvider odataProvider = new OdataProvider();

            // Act
            var result = odataProvider.GetFilterPredicate(filter);

            // Assert
            result.Should().NotBeNull();
        }
    }
}

