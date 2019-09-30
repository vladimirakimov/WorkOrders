using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class DeleteWorkOrderRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var route = new DeleteWorkOrderFromRoute();
            var query = new DeleteWorkOrderFromQuery();
            var header = new DeleteWorkOrderFromHeader();


            // Act
            var obj = new DeleteWorkOrderRequest(route, query, header);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRouteIsNull()
        {
            // Arrange
            DeleteWorkOrderFromRoute route = null;
            var query = new DeleteWorkOrderFromQuery();
            var header = new DeleteWorkOrderFromHeader();

            // Act
            Action ctor = () => { new DeleteWorkOrderRequest(route, query, header); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            var route = new DeleteWorkOrderFromRoute();
            DeleteWorkOrderFromQuery query = null;
            var header = new DeleteWorkOrderFromHeader();

            // Act
            Action ctor = () => { new DeleteWorkOrderRequest(route, query, header); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenHeaderIsNull()
        {
            // Arrange
            var route = new DeleteWorkOrderFromRoute();
            var query = new DeleteWorkOrderFromQuery();
            DeleteWorkOrderFromHeader header = null;

            // Act
            Action ctor = () => { new DeleteWorkOrderRequest(route, query, header); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberRouteIdShouldHaveCorrectValue()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var route = new DeleteWorkOrderFromRoute() { Id = routeId };
            var query = new DeleteWorkOrderFromQuery();
            var header = new DeleteWorkOrderFromHeader();

            // Act
            var obj = new DeleteWorkOrderRequest(route, query, header);

            // Assert
            obj.RouteId.Should().Be(routeId);
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var route = new DeleteWorkOrderFromRoute();
            var query = new DeleteWorkOrderFromQuery() { ApiVersion = apiVersion };
            var header = new DeleteWorkOrderFromHeader();

            // Act
            var obj = new DeleteWorkOrderRequest(route, query, header);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }

        [TestMethod]
        public void MemberHeaderIfMatchShouldHaveCorrectValue()
        {
            // Arrange
            var ifMatch = "287687687";
            var route = new DeleteWorkOrderFromRoute();
            var query = new DeleteWorkOrderFromQuery();
            var header = new DeleteWorkOrderFromHeader() { IfMatch = ifMatch };

            // Act
            var obj = new DeleteWorkOrderRequest(route, query, header);

            // Assert
            obj.HeaderIfMatch.Should().Be(ifMatch);
        }
    }
}
