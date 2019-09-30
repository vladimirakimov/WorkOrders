using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class IdentifierProviderTests
    {
        [TestMethod]
        public void GenerateShouldSucceed()
        {
            // Arrange
            IIdentifierProvider identifierProvider = new IdentifierProvider();

            // Act
            var result = identifierProvider.Generate();

            // Assert
            result.Should().NotBeEmpty();
        }
    }
}
