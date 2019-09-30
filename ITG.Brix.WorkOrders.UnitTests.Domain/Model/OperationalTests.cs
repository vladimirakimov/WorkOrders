using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class OperationalTests
    {
        [TestMethod]
        public void CreateOperationalShouldSucceed()
        {
            // Arrange
            var status = Status.Blocked;

            // Act
            var result = new Operational(status);

            // Assert
            result.Should().NotBeNull();
        }
    }
}
