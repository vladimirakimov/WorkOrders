using FluentAssertions;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Exceptions
{
    [TestClass]
    public class ValueShouldNotBeEmptyExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = ExceptionMessage.ItemValueNullOrWhitespace;

            // Act
            var exception = new ItemValueNullOrWhitespaceException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
