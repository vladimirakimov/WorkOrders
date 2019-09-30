using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class VersionProviderTests
    {
        [TestMethod]
        public void GenerateShouldBeInRange()
        {
            // Arrange
            IVersionProvider versionProvider = new VersionProvider();

            // Act
            var result = versionProvider.Generate();

            // Assert
            result.Should().BeInRange(1000000000, int.MaxValue);
        }
    }
}
