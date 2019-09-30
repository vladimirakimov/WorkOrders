using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class ItemKeyTests
    {
        [DataTestMethod]
        [DataRow("#business_key")]
        [DataRow("#businesskey")]
        [DataRow("#businesskey1")]
        [DataRow("#businesskey10")]
        [DataRow("#business_key_")]
        [DataRow("#_business_key")]
        [DataRow("#_business_key_")]
        public void CreateItemKeyShouldSucceed(string businessKey)
        {
            // Arrange
            var value = businessKey;

            // Act
            Action ctor = () => { new ItemKey(value); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void CreateItemKeyShouldFailWhenValueIsNull()
        {
            // Arrange
            string value = null;

            // Act
            Action ctor = () => { new ItemKey(value); };

            // Assert
            ctor.Should().Throw<ItemValueNullOrWhitespaceException>();
        }

        [TestMethod]
        public void CreateItemKeyShouldFailWhenValueIsStringEmpty()
        {
            // Arrange
            var value = string.Empty;

            // Act
            Action ctor = () => { new ItemKey(value); };

            // Assert
            ctor.Should().Throw<ItemValueNullOrWhitespaceException>();
        }

        [TestMethod]
        public void CreateItemKeyShouldFailWhenValueIsWhitespace()
        {
            // Arrange
            string value = "   ";

            // Act
            Action ctor = () => { new ItemKey(value); };

            // Assert
            ctor.Should().Throw<ItemValueNullOrWhitespaceException>();
        }

        [DataTestMethod]
        [DataRow("Business_key")]
        [DataRow("business-key")]
        [DataRow("business#key")]
        [DataRow("BusinessKey")]
        public void CreateItemKeyShouldFailWhenValueHasWrongFormat(string businessKey)
        {
            // Arrange
            var value = businessKey;

            // Act
            Action ctor = () => { new ItemKey(value); };

            // Assert
            ctor.Should().Throw<ItemValueFormatException>();
        }

    }
}
