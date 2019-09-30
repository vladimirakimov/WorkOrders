using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Location
{
    [TestClass]
    public class GateTests
    {
        [TestMethod]
        public void CreateGateShouldSucceed()
        {
            // Arrange
            var value = "Gate";
            var label = new Label(value);

            // Act
            var result = new Gate(label);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateGateShouldFailWhenLabelIsNull()
        {
            // Arrange
            Label label = null;

            // Act
            Action ctor = () => { new Gate(label); };

            // Assert
            ctor.Should().Throw<GateLabelFieldShouldNotBeNullException>();
        }

        [TestMethod]
        public void OperatorToStringShouldReturnCorrectValue()
        {
            // Arrange
            var value = "Gate";
            var label = new Label(value);
            var gate = new Gate(label);

            // Act
            string result = gate;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToStringShouldReturnUnsetValue()
        {
            // Arrange
            Gate gate = null;

            // Act
            string result = gate;

            // Assert
            result.Should().Be(Label.UnsetValue);
        }

    }
}
