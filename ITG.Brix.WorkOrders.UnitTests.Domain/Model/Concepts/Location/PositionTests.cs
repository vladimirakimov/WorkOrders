using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Location
{
    [TestClass]
    public class PositionTests
    {
        [TestMethod]
        public void CreatePositionShouldSucceed()
        {
            // Arrange
            var value = "Position";
            var label = new Label(value);

            // Act
            var result = new Position(label);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreatePositionShouldFailWhenLabelIsNull()
        {
            // Arrange
            Label label = null;

            // Act
            Action ctor = () => { new Position(label); };

            // Assert
            ctor.Should().Throw<PositionLabelFieldShouldNotBeNullException>();
        }

        [TestMethod]
        public void OperatorToStringShouldReturnCorrectValue()
        {
            // Arrange
            var value = "Position";
            var label = new Label(value);
            var position = new Position(label);

            // Act
            string result = position;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToStringShouldReturnUnsetValue()
        {
            // Arrange
            Position position = null;

            // Act
            string result = position;

            // Assert
            result.Should().Be(Label.UnsetValue);
        }

    }
}
