using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class CreatedOnTests
    {
        [TestMethod]
        public void CreateCreatedOnShouldSucceed()
        {
            // Arrange
            var date = DateTime.UtcNow;

            // Act
            var newDate = new CreatedOn(date);

            // Assert
            newDate.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateCreatedOnShouldFailWhenValueIsNotUtc()
        {
            // Arrange
            var value = new DateTime(2019, 1, 1);

            // Act
            Action ctor = () => { new CreatedOn(value); };

            // Assert
            ctor.Should().Throw<CreatedOnValueFieldShouldBeValidException>();
        }

        [TestMethod]
        public void CreateCreatedOnShouldFailWhenValueIsLessThanAllowed()
        {
            // Arrange
            var value = new DateTime(1899, 1, 1).ToUniversalTime();

            // Act
            Action ctor = () => { new CreatedOn(value); };

            // Assert
            ctor.Should().Throw<CreatedOnValueFieldShouldBeValidException>();
        }

        [TestMethod]
        public void OperatorToDateTimeShouldSucceed()
        {
            // Arrange
            var value = DateTime.Now.ToUniversalTime();
            var createdOn = new CreatedOn(value);

            // Act
            DateTime result = createdOn;

            // Assert
            result.Should().Be(value);
        }
    }
}
