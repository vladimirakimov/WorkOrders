using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts
{
    [TestClass]
    public class UnitsTests
    {
        [TestMethod]
        public void CreateUnitsShouldSucceed()
        {
            // Arrange
            var value = 55;

            // Act
            var result = new Units(value);

            // Assert
            result.Should().Be(new Units(value));
        }

        [TestMethod]
        public void CreateUnitsShouldFailWhenValueIsLessThanZero()
        {
            // Arrange
            var value = -1;

            // Act
            Action ctor = () => { new Units(value); };

            // Assert
            ctor.Should().Throw<UnitsValueFieldShouldBeGreaterOrEqualToZeroException>();
        }

        [TestMethod]
        public void OperatorToIntShouldSucceed()
        {
            // Arrange
            var value = 55;
            var units = new Units(value);

            // Act
            int result = units;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToIntShouldFailWhenUnitsIsNull()
        {
            // Arrange
            Units units = null;

            // Act
            Action action = () => { int result = units; };

            // Assert
            action.Should().Throw<UnitsShouldNotBeNullException>();
        }
    }
}
