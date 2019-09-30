using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts
{
    [TestClass]
    public class WeightTests
    {
        [TestMethod]
        public void CreateWeightShouldSucceed()
        {
            // Arrange
            var weight = 10.3F;

            // Act
            var result = new Weight(weight);

            // Assert
            result.Should().Be(new Weight(weight));
        }

        [TestMethod]
        public void CreateWeightShouldFailWhenValueIsLessThanZero()
        {
            // Arrange
            var weight = -0.1F;

            // Act
            Action ctor = () => { new Weight(weight); };

            // Assert
            ctor.Should().Throw<WeightValueFieldShouldBeGreaterOrEqualToZeroException>();
        }

        [TestMethod]
        public void OperatorToStringShouldSucceed()
        {
            // Arrange
            var weight = new Weight(10.3F);

            // Act
            string x = weight;

            // Assert
            x.Should().Be("10.30");
        }

        [TestMethod]
        public void OperatorDivideByFloatShouldSucceed()
        {
            // Arrange
            var weight = new Weight(9.0F);

            // Act
            var result = weight / 3;

            // Assert
            result.Should().Be(new Weight(3.00F));
        }
    }
}
