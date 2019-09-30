using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Exceptions
{
    [TestClass]
    public class BiztalkCallExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = "BiztalkCall";

            // Act
            var exception = new BiztalkCallException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
