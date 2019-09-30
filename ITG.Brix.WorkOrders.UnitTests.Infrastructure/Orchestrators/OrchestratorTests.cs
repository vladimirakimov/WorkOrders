using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrations;
using ITG.Brix.WorkOrders.Infrastructure.Orchestrators.Impl;
using ITG.Brix.WorkOrders.Infrastructure.RestApis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Orchestrators
{

    [TestClass]
    public class OrchestratorTests
    {
        [TestMethod]
        public void ConstructorShouldSucceed()
        {
            // Arrange
            var biztalkRestApi = new Mock<IBiztalkRestApi>().Object;
            var biztalkOrchestration = new Mock<IBiztalkOrchestration>().Object;

            // Act
            Action ctor = () => { new Orchestrator(biztalkRestApi, biztalkOrchestration); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBiztalkRestApiIsNull()
        {
            // Arrange
            IBiztalkRestApi biztalkRestApi = null;
            var biztalkOrchestration = new Mock<IBiztalkOrchestration>().Object;

            // Act
            Action ctor = () => { new Orchestrator(biztalkRestApi, biztalkOrchestration); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }

        [TestMethod]
        public void ConstructorShouldFailWhenBiztalkOrchestrationIsNull()
        {
            // Arrange
            var biztalkRestApi = new Mock<IBiztalkRestApi>().Object;
            IBiztalkOrchestration biztalkOrchestration = null;

            // Act
            Action ctor = () => { new Orchestrator(biztalkRestApi, biztalkOrchestration); };

            // Assert
            ctor.Should().Throw<ArgumentNullException>();
        }
    }
}
