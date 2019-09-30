using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class WorkOrderProviderTests
    {
        [TestMethod]
        public void GenerateShouldBeInRange()
        {
            // Arrange
            IWorkOrderProvider objectProvider = new WorkOrderProvider();

            // Act
            var result = objectProvider.GetPropertyTypePairs();

            // Assert
            result.Should().NotBeNull();
        }
    }
}
