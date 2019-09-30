using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Exceptions
{
    [TestClass]
    public class PlatoCallExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = ExceptionMessage.PlatoCall;

            // Act
            var exception = new PlatoCallException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
