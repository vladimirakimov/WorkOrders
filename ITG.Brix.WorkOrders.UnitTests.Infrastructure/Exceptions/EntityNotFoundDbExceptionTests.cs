using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Exceptions
{
    [TestClass]
    public class EntityNotFoundDbExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = ExceptionMessage.EntityNotFoundDb;

            // Act
            var exception = new EntityNotFoundDbException();

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
