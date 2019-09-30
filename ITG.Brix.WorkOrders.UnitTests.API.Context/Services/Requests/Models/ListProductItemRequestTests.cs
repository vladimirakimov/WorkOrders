using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class ListProductItemRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new ListProductItemFromQuery();


            // Act
            var obj = new ListProductItemRequest(query);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            ListProductItemFromQuery query = null;

            // Act
            Action ctor = () => { new ListProductItemRequest(query); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var query = new ListProductItemFromQuery() { ApiVersion = apiVersion };

            // Act
            var obj = new ListProductItemRequest(query);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }
    }
}
