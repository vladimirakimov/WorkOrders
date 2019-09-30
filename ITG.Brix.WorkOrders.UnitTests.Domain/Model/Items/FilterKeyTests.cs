using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class FilterKeyTests
    {
        [DataTestMethod]
        [DataRow("filter-key")]
        [DataRow("filterkey")]
        [DataRow("filter-filter-")]
        [DataRow("-filter-key")]
        [DataRow("-filter-filter-")]
        public void CreateFilterKeyShouldSucceed(string filterKey)
        {
            // Arrange
            var value = filterKey;

            // Act
            Action ctor = () => { new FilterKey(value); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void CreateFilterKeyShouldFailWhenValueIsNull()
        {
            // Arrange
            string value = null;

            // Act
            Action ctor = () => { new FilterKey(value); };

            // Assert
            ctor.Should().Throw<FilterKeyValueNullOrWhitespaceException>();
        }

        [TestMethod]
        public void CreateFilterKeyShouldFailWhenValueIsStringEmpty()
        {
            // Arrange
            var value = string.Empty;

            // Act
            Action ctor = () => { new FilterKey(value); };

            // Assert
            ctor.Should().Throw<FilterKeyValueNullOrWhitespaceException>();
        }

        [TestMethod]
        public void CreateFilterKeyShouldFailWhenValueIsWhitespace()
        {
            // Arrange
            string value = "   ";

            // Act
            Action ctor = () => { new FilterKey(value); };

            // Assert
            ctor.Should().Throw<FilterKeyValueNullOrWhitespaceException>();
        }

        [DataTestMethod]
        [DataRow("Filter-key")]
        [DataRow("filter_key")]
        [DataRow(@"filter\key")]
        [DataRow("filter#key")]
        [DataRow("FilterKey")]
        public void CreateItemKeyShouldFailWhenValueHasWrongFormat(string filterKey)
        {
            // Arrange
            var value = filterKey;

            // Act
            Action ctor = () => { new FilterKey(value); };

            // Assert
            ctor.Should().Throw<FilterKeyValueFormatException>();
        }

    }
}
