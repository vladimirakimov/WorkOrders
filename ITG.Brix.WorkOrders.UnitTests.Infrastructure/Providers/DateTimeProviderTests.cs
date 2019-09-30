using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class DateTimeProviderTests
    {

        #region ParseUtc

        [TestMethod]
        public void ParseUtcShouldSucceed()
        {
            // Arrange
            var format = "yyyy-MM-ddTHH:mm:ssZ";
            var dateTimeUtc = DateTime.UtcNow;
            var utc = dateTimeUtc.ToString(format);
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();

            // Act
            var result = dateTimeProvider.ParseUtc(utc);

            // Assert
            result.Should().BeSameDateAs(dateTimeUtc);
            result.Kind.Should().Be(DateTimeKind.Utc);
        }

        //[DataTestMethod]
        //[DataRow(null)]
        //[DataRow("")]
        //[DataRow("   ")]
        //[DataRow("6/19/2019 10:35:50 AM")]
        //[DataRow("2019-05-10T05:04:10.40")]
        //[DataRow("019-05-10T05:04:10.4Z")]
        //[DataRow("2019-05-10T05:04:10")]
        //[DataRow("2019-05-10")]
        //[DataRow("invalid")]
        //public void ParseUtcShouldFail(string utc)
        //{
        //    // Arrange
        //    IDateTimeProvider dateTimeProvider = new DateTimeProvider();

        //    // Act
        //    Action action = () => { dateTimeProvider.ParseUtc(utc); };

        //    // Assert
        //    action.Should().Throw<Exception>();
        //}

        #endregion
        [TestMethod]
        public void ParseShouldSucceed()
        {
            // Arrange
            var dateTimeIso8601 = "2019-01-30T06:48:50Z";
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();


            // Act
            var result = dateTimeProvider.Parse(dateTimeIso8601);

            // Assert
            result.HasValue.Should().BeTrue();
        }


        [DataTestMethod]
        [DataRow("6/19/2019 10:35:50 AM")]
        [DataRow("2019-05-10T05:04:10.40")]
        [DataRow("019-05-10T05:04:10.4Z")]
        [DataRow("2019-05-10T05:04:10")]
        [DataRow("2019-05-10")]
        [DataRow("invalid")]
        public void CheckFormatShouldReturnFalse(string dateTimeIso8601)
        {
            // Arrange
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();

            // Act
            var result = dateTimeProvider.CheckFormat(dateTimeIso8601);

            // Assert
            result.Should().BeFalse();
        }

        [DataTestMethod]
        [DataRow(null)]
        [DataRow("")]
        [DataRow("  ")]
        public void CheckFormatShouldFail(string dateTimeIso8601)
        {

            // Arrange
            IDateTimeProvider dateTimeProvider = new DateTimeProvider();


            // Act
            Action action = () => { dateTimeProvider.CheckFormat(dateTimeIso8601); };

            // Assert
            action.Should().Throw<Exception>();
        }
    }
}
