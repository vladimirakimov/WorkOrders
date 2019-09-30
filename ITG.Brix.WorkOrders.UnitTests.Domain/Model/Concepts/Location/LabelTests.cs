using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain.Model.Concepts.Location
{
    [TestClass]
    public class LabelTests
    {
        [TestMethod]
        public void CreateLabelShouldSucceed()
        {
            // Arrange
            var value = "Label";

            // Act
            var result = new Label(value);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateLabelShouldFailWhenValueIsNull()
        {
            // Arrange
            string value = null;

            // Act
            Action ctor = () => { new Label(value); };

            // Assert
            ctor.Should().Throw<LabelValueFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateLabelShouldFailWhenValueIsStringEmpty()
        {
            // Arrange
            var value = string.Empty;

            // Act
            Action ctor = () => { new Label(value); };

            // Assert
            ctor.Should().Throw<LabelValueFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void CreateLabelShouldFailWhenValueIsWhitespace()
        {
            // Arrange
            var value = "   ";

            // Act
            Action ctor = () => { new Label(value); };

            // Assert
            ctor.Should().Throw<LabelValueFieldShouldNotBeEmptyException>();
        }

        [TestMethod]
        public void OperatorToStringShouldReturnCorrectValue()
        {
            // Arrange
            var value = "Value";
            var label = new Label(value);

            // Act
            string result = label;

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void OperatorToStringShouldReturnUnsetValue()
        {
            // Arrange
            Label label = null;

            // Act
            string result = label;

            // Assert
            result.Should().Be(Label.UnsetValue);
        }
    }
}
