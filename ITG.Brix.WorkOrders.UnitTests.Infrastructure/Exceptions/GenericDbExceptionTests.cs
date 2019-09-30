using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Exceptions
{
    [TestClass]
    public class GenericDbExceptionTests
    {
        [TestMethod]
        public void ShouldHavePredefinedMessage()
        {
            // Arrange
            var expectedMessage = ExceptionMessage.GenericDb;

            // Act
            var exception = new GenericDbException(new ArgumentNullException());

            // Assert
            exception.Message.Should().Be(expectedMessage);
        }
    }
}
