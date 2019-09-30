using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Exceptions
{
    [TestClass]
    public class EntityVersionDbExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = ExceptionMessage.EntityVersionDb;

            // Act
            var exception = new EntityVersionDbException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
