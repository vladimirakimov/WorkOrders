using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Operants
{
    [TestClass]
    public class LoginTests
    {
        [TestMethod]
        public void CreateLoginShouldSucceed()
        {
            // Arrange
            var value = "Login";

            // Act
            var result = new Login(value);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateLoginShouldFailWhenValueIsNull()
        {
            // Arrange
            string value = null;

            // Act
            Action ctor = () => { new Login(value); };

            // Assert
            ctor.Should().Throw<LoginValueFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateLoginShouldFailWhenValueIsStringEmpty()
        {
            // Arrange
            var value = string.Empty;

            // Act
            Action ctor = () => { new Login(value); };

            // Assert
            ctor.Should().Throw<LoginValueFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateLoginShouldFailWhenValueIsWhitespace()
        {
            // Arrange
            var value = "   ";

            // Act
            Action ctor = () => { new Login(value); };

            // Assert
            ctor.Should().Throw<LoginValueFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void OperatorToStringShouldReturnCorrectValue()
        {
            // Arrange
            var value = "Value";
            var login = new Login(value);

            // Act
            string result = login;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToStringShouldReturnUnsetValue()
        {
            // Arrange
            Login login = null;

            // Act
            string result = login;

            // Assert
            result.Should().Be(Login.UnsetLogin);
        }
    }
}
