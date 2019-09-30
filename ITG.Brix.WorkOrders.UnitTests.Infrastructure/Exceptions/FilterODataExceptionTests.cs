using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Exceptions
{
    [TestClass]
    public class FilterODataExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = ExceptionMessage.FilterOData;

            // Act
            var exception = new FilterODataException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
