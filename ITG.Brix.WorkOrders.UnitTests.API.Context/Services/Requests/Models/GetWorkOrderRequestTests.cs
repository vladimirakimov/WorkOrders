using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class GetWorkOrderRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var route = new GetWorkOrderFromRoute();
            var query = new GetWorkOrderFromQuery();


            // Act
            var obj = new GetWorkOrderRequest(route, query);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRouteIsNull()
        {
            // Arrange
            GetWorkOrderFromRoute route = null;
            var query = new GetWorkOrderFromQuery();

            // Act
            Action ctor = () => { new GetWorkOrderRequest(route, query); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            var route = new GetWorkOrderFromRoute();
            GetWorkOrderFromQuery query = null;

            // Act
            Action ctor = () => { new GetWorkOrderRequest(route, query); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberRouteIdShouldHaveCorrectValue()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var route = new GetWorkOrderFromRoute() { Id = routeId };
            var query = new GetWorkOrderFromQuery();

            // Act
            var obj = new GetWorkOrderRequest(route, query);

            // Assert
            obj.RouteId.Should().Be(routeId);
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var route = new GetWorkOrderFromRoute();
            var query = new GetWorkOrderFromQuery() { ApiVersion = apiVersion };

            // Act
            var obj = new GetWorkOrderRequest(route, query);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }
    }
}
