using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class ListWorkOrderRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new ListWorkOrderFromQuery();


            // Act
            var obj = new ListWorkOrderRequest(query);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            ListWorkOrderFromQuery query = null;

            // Act
            Action ctor = () => { new ListWorkOrderRequest(query); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var query = new ListWorkOrderFromQuery() { ApiVersion = apiVersion };

            // Act
            var obj = new ListWorkOrderRequest(query);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }

        [DataTestMethod]
        [DataRow("BKAL33+KBT T")]
        [DataRow("BKAL33+KBT/T")]
        [DataRow("BKAL33+KBT?T")]
        [DataRow("BKAL33+KBT%T")]
        [DataRow("BKAL33+KBT&T")]
        [DataRow("BKAL33+KBT'T")]
        [DataRow("BKAL33+KBT\"T")]
        [DataRow("BKAL33+KBT<T")]
        [DataRow("BKAL33+KBT>T")]
        public void MemberQueryFilterShouldUnescapeToCorrectValue(string filter)
        {
            // Arrange
            var apiVersion = "1.0";
            var query = new ListWorkOrderFromQuery() { ApiVersion = apiVersion, Filter = Uri.EscapeDataString(filter) };

            // Act
            var obj = new ListWorkOrderRequest(query);
            obj.Unescape();

            // Assert
            obj.Filter.Should().Be(filter);
        }
    }
}
