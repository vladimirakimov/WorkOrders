using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class DateOnTests
    {
        [TestMethod]
        public void CreateDateOnShouldSucceed()
        {
            // Arrange
            var date = DateTime.UtcNow;

            // Act
            var newDate = new DateOn(date);

            // Assert
            newDate.Should().NotBeNull();
        }

        [TestMethod]
        public void CreateDateTimeOnShouldFailWhenValueIsNotUtc()
        {
            // Arrange
            var value = new DateTime(2019, 1, 1);

            // Act
            Action ctor = () => { new DateOn(value); };

            // Assert
            ctor.Should().Throw<DateOnValueFieldShouldBeValidException>();
        }

        [TestMethod]
        public void CreateDateTimeOnShouldFailWhenValueIsLessThanAllowed()
        {
            // Arrange
            var value = new DateTime(1900, 1, 1).ToUniversalTime();

            // Act
            Action ctor = () => { new DateOn(value); };

            // Assert
            ctor.Should().Throw<DateOnValueFieldShouldBeValidException>();
        }
    }
}
