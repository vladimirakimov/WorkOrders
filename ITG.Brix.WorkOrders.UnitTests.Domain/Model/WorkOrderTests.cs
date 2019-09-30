using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class WorkOrderTests
    {
        private readonly CreatedOn startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
        private readonly CreatedOn stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());

        [TestMethod]
        public void CreateWorkOrderShouldSucceed()
        {
            // Arrange
            var isEditable = true;
            var order = new Order();
            var operational = new Operational(Status.Open);
            var userCreated = "besliu";

            // Act
            new WorkOrder.Builder()
                        .WithId(Guid.NewGuid())
                        .WithIsEditable(isEditable)
                        .WithOrder(order)
                        .WithOperational(operational)
                        .WithUserCreated(userCreated)
                        .WithCreatedOn(startCreatedOn)
                        .Build();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void CreateWorkOrderShouldFailWhenOrderIsNull()
        {
            // Arrange
            var isEditable = true;
            Order order = null;
            var operational = new Operational(Status.Open);
            var userCreated = "besliu";

            // Act
            new WorkOrder.Builder()
                            .WithId(Guid.NewGuid())
                            .WithIsEditable(isEditable)
                            .WithOrder(order)
                            .WithOperational(operational)
                            .WithUserCreated(userCreated)
                            .WithCreatedOn(startCreatedOn)
                            .Build();
        }

        [TestMethod]
        public void CreateWorkOrderShouldFailWhenIdIsGuidEmpty()
        {
            // Arrange
            var isEditable = true;
            var order = new Order();
            var operational = new Operational(Status.Open);
            var userCreated = "besliu";

            // Act
            Action ctor = () =>
            {
                new WorkOrder.Builder()
                    .WithId(Guid.Empty)
                    .WithIsEditable(isEditable)
                    .WithOrder(order)
                    .WithOperational(operational)
                    .WithUserCreated(userCreated)
                    .WithCreatedOn(startCreatedOn)
                    .Build();
            };

            // Assert
            ctor.Should().Throw<IdFieldShouldNotBeDefaultGuidException>();
        }
    }
}
