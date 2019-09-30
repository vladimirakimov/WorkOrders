using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Exceptions
{
    [TestClass]
    public class CommandVersionExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = "CommandVersion";

            // Act
            var exception = new CommandVersionException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
