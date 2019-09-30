using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Location
{
    [TestClass]
    public class WarehouseTests
    {
        [TestMethod]
        public void CreateWarehouseShouldSucceed()
        {
            // Arrange
            var value = "Warehouse";
            var label = new Label(value);

            // Act
            var result = new Warehouse(label);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateWarehouseShouldFailWhenLabelIsNull()
        {
            // Arrange
            Label label = null;

            // Act
            Action ctor = () => { new Warehouse(label); };

            // Assert
            ctor.Should().Throw<WarehouseLabelFieldShouldNotBeNullException>();
        }

        [TestMethod]
        public void OperatorToStringShouldReturnCorrectValue()
        {
            // Arrange
            var value = "Warehouse";
            var label = new Label(value);
            var warehouse = new Warehouse(label);

            // Act
            string result = warehouse;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToStringShouldReturnUnsetValue()
        {
            // Arrange
            Warehouse warehouse = null;

            // Act
            string result = warehouse;

            // Assert
            result.Should().Be(Label.UnsetValue);
        }

    }
}
