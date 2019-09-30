using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Operants
{
    [TestClass]
    public class OperantTests
    {
        [TestMethod]
        public void CreateOperantShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var loginValue = "Login";
            var login = new Login(loginValue);

            // Act
            var result = new Operant(id, login);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Login.Should().Be(login);
        }

        [TestMethod]
        public void CreateOperantShouldFailWhenIdDefaultGuid()
        {
            // Arrange
            var id = Guid.Empty;
            var loginValue = "Login";
            var login = new Login(loginValue);

            // Act
            Action ctor = () => { new Operant(id, login); };

            // Assert
            ctor.Should().Throw<OperantIdFieldShouldNotBeDefaultGuidException>();
        }

        [TestMethod]
        public void CreateOperantShouldFailWhenLoginIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            Login login = null;

            // Act
            Action ctor = () => { new Operant(id, login); };

            // Assert
            ctor.Should().Throw<OperantLoginFieldShouldNotBeNullException>();
        }
    }
}
