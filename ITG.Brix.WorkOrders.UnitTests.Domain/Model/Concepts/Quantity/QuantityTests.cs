using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts
{
    [TestClass]
    public class QuantityTests
    {
        [TestMethod]
        public void CreateQuantityShouldSucceed()
        {
            // Arrange
            var value = 55;

            // Act
            var result = new Quantity(value);

            // Assert
            result.Should().Be(new Quantity(value));
        }

        [TestMethod]
        public void CreateQuantityShouldFailWhenValueIsLessThanZero()
        {
            // Arrange
            var value = -1;

            // Act
            Action ctor = () => { new Quantity(value); };

            // Assert
            ctor.Should().Throw<QuantityValueFieldShouldBeGreaterOrEqualToZeroException>();
        }

        [TestMethod]
        public void OperatorToIntShouldSucceed()
        {
            // Arrange
            var value = 55;
            var quantity = new Quantity(value);

            // Act
            int result = quantity;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToIntShouldFailWhenQuantityIsNull()
        {
            // Arrange
            Quantity quantity = null;

            // Act
            Action action = () => { int result = quantity; };

            // Assert
            action.Should().Throw<QuantityShouldNotBeNullException>();
        }
    }
}
