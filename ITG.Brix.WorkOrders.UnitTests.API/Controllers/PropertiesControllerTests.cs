using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Services;
using ITG.Brix.WorkOrders.API.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Controllers
{
    [TestClass]
    public class PropertiesControllerTests
    {
        [TestMethod]
        public void ConstructorShouldRegisterAllDependencies()
        {
            // Arrange
            var apiResult = new Mock<IApiResult>().Object;

            // Act
            var result = new PropertiesController(apiResult);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenApiResultNull()
        {
            // Arrange
            IApiResult apiResult = null;
            // Act
            Action ctor = () => { new PropertiesController(apiResult); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
