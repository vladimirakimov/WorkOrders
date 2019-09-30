using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class CreateWorkOrderRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var query = new CreateWorkOrderFromQuery();
            var body = new CreateWorkOrderFromBody();

            // Act
            var obj = new CreateWorkOrderRequest(query, body);

            // Assert
            obj.Should().NotBeNull();
        }


        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            CreateWorkOrderFromQuery query = null;
            var body = new CreateWorkOrderFromBody();

            // Act
            Action ctor = () => { new CreateWorkOrderRequest(query, body); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBodyIsNull()
        {
            // Arrange
            var query = new CreateWorkOrderFromQuery();
            CreateWorkOrderFromBody body = null;

            // Act
            Action ctor = () => { new CreateWorkOrderRequest(query, body); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var query = new CreateWorkOrderFromQuery() { ApiVersion = apiVersion };
            var body = new CreateWorkOrderFromBody();

            // Act
            var obj = new CreateWorkOrderRequest(query, body);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }
    }
}
