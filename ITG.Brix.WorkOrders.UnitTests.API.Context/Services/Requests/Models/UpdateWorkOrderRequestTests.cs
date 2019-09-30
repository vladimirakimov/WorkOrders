using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Models.From;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Models
{
    [TestClass]
    public class UpdateWorkOrderRequestTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var route = new UpdateWorkOrderFromRoute();
            var query = new UpdateWorkOrderFromQuery();
            var header = new UpdateWorkOrderFromHeader();
            var body = new UpdateWorkOrderFromBody();

            // Act
            var obj = new UpdateWorkOrderRequest(route, query, header, body);

            // Assert
            obj.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenRouteIsNull()
        {
            // Arrange
            UpdateWorkOrderFromRoute route = null;
            var query = new UpdateWorkOrderFromQuery();
            var header = new UpdateWorkOrderFromHeader();
            var body = new UpdateWorkOrderFromBody();

            // Act
            Action ctor = () => { new UpdateWorkOrderRequest(route, query, header, body); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenQueryIsNull()
        {
            // Arrange
            var route = new UpdateWorkOrderFromRoute();
            UpdateWorkOrderFromQuery query = null;
            var header = new UpdateWorkOrderFromHeader();
            var body = new UpdateWorkOrderFromBody();

            // Act
            Action ctor = () => { new UpdateWorkOrderRequest(route, query, header, body); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenHeaderIsNull()
        {
            // Arrange
            var route = new UpdateWorkOrderFromRoute();
            var query = new UpdateWorkOrderFromQuery();
            UpdateWorkOrderFromHeader header = null;
            var body = new UpdateWorkOrderFromBody();

            // Act
            Action ctor = () => { new UpdateWorkOrderRequest(route, query, header, body); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBodyIsNull()
        {
            // Arrange
            var route = new UpdateWorkOrderFromRoute();
            var query = new UpdateWorkOrderFromQuery();
            var header = new UpdateWorkOrderFromHeader();
            UpdateWorkOrderFromBody body = null;

            // Act
            Action ctor = () => { new UpdateWorkOrderRequest(route, query, header, body); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void MemberRouteIdShouldHaveCorrectValue()
        {
            // Arrange
            var routeId = Guid.NewGuid().ToString();
            var route = new UpdateWorkOrderFromRoute() { Id = routeId };
            var query = new UpdateWorkOrderFromQuery();
            var header = new UpdateWorkOrderFromHeader();
            var body = new UpdateWorkOrderFromBody();

            // Act
            var obj = new UpdateWorkOrderRequest(route, query, header, body);

            // Assert
            obj.RouteId.Should().Be(routeId);
        }

        [TestMethod]
        public void MemberQueryApiVersionShouldHaveCorrectValue()
        {
            // Arrange
            var apiVersion = "1.0";
            var route = new UpdateWorkOrderFromRoute();
            var query = new UpdateWorkOrderFromQuery() { ApiVersion = apiVersion };
            var header = new UpdateWorkOrderFromHeader();
            var body = new UpdateWorkOrderFromBody();

            // Act
            var obj = new UpdateWorkOrderRequest(route, query, header, body);

            // Assert
            obj.QueryApiVersion.Should().Be(apiVersion);
        }

        [TestMethod]
        public void MembersHeaderShouldHaveCorrectValue()
        {
            // Arrange
            var contentType = "application/json";
            var ifMatch = "3452353445435";

            var route = new UpdateWorkOrderFromRoute();
            var query = new UpdateWorkOrderFromQuery();
            var header = new UpdateWorkOrderFromHeader() { IfMatch = ifMatch, ContentType = contentType };
            var body = new UpdateWorkOrderFromBody();


            // Act
            var obj = new UpdateWorkOrderRequest(route, query, header, body);

            // Assert
            obj.HeaderContentType.Should().Be(contentType);
            obj.HeaderIfMatch.Should().Be(ifMatch);
        }
    }
}
