using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class ListOrderItemRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new ListOrderItemFromQuery();


            // Act
            var obj = new ListOrderItemRequest(query);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            ListOrderItemFromQuery query = null;

            // Act
            Action ctor = () => { new ListOrderItemRequest(query); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var query = new ListOrderItemFromQuery() { ApiVersion = apiVersion };

            // Act
            var obj = new ListOrderItemRequest(query);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }
    }
}
