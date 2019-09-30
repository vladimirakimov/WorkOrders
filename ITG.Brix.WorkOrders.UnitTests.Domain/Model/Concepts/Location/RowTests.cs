using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Location
{
    [TestClass]
    public class RowTests
    {
        [TestMethod]
        public void CreateRowShouldSucceed()
        {
            // Arrange
            var value = "Row";
            var label = new Label(value);

            // Act
            var result = new Row(label);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateRowShouldFailWhenLabelIsNull()
        {
            // Arrange
            Label label = null;

            // Act
            Action ctor = () => { new Row(label); };

            // Assert
            ctor.Should().Throw<RowLabelFieldShouldNotBeNullException>();
        }

        [TestMethod]
        public void OperatorToStringShouldReturnCorrectValue()
        {
            // Arrange
            var value = "Row";
            var label = new Label(value);
            var row = new Row(label);

            // Act
            string result = row;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToStringShouldReturnUnsetValue()
        {
            // Arrange
            Row row = null;

            // Act
            string result = row;

            // Assert
            result.Should().Be(Label.UnsetValue);
        }

    }
}
