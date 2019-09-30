using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using ITG.Brix.WorkOrders.Infrastructure.DataAccess.ClassMaps;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ITG.Brix.WorkOrders.IntegrationTests.Infrastructure.DataAccess.ClassMaps
{
    [TestClass]
    public class WorkOrderClassMapTests
    {
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            ClassMapsRegistrator.RegisterMaps();
        }

        [TestMethod]
        public void WorkOrderToAndFromBsonShouldSucceed()
        {
            // Arrange
            var id = Guid.NewGuid();
            var order = new Order()
            {
                Number = "Number",
                //Products = new List<Product>()
                //{
                //    new Product()
                //    {
                //        Code = "Product1Code",
                //        Remarks = new List<string>(){"remark11", "remark12"}
                //    },
                //    new Product()
                //    {
                //        Code = "Product2Code",
                //        Remarks = new List<string>(){"remark21", "remark22"}
                //    }
                //},
                Operation = new Operation()
                {
                    ExtraActivities = new List<ExtraActivity>()
                    {
                        new ExtraActivity()
                        {
                            Code = "ExtraActivity01"
                        },
                        new ExtraActivity()
                        {
                            Code = "ExtraActivity02"
                        }
                    },
                    OperationalRemarks = new List<string>() { "operationalRemark1", "operationalRemark2" }
                }
            };
            var startCreatedOn = new CreatedOn(DateTime.Now.AddHours(-5).ToUniversalTime());
            var stopCreatedOn = new CreatedOn(DateTime.Now.AddHours(-3).ToUniversalTime());
            var operational = new Operational(Status.Open);
            var workOrder = new WorkOrder(id, true, order, operational, "anyUser", startCreatedOn);


            // Act
            var bson = workOrder.ToBson();
            var rehydrated = BsonSerializer.Deserialize<WorkOrder>(bson);

            // Assert
            rehydrated.Should().NotBeNull();
            rehydrated.Id.Should().Be(id);
            rehydrated.UserCreated.Should().Be("anyUser");
            rehydrated.Order.Should().NotBeNull();
            rehydrated.Order.Number.Should().Be(order.Number);
            //rehydrated.Order.Products.Should().NotBeNull();
            //rehydrated.Order.Products.Count().Should().Be(order.Products.Count());
            //rehydrated.Order.Products.Any(x => x.Code == order.Products.First().Code).Should().BeTrue();
            //rehydrated.Order.Products.Any(x => x.Code == order.Products.Last().Code).Should().BeTrue();
            //rehydrated.Order.Products.First().Remarks.Should().NotBeNull();
            //rehydrated.Order.Products.First().Remarks.Count().Should().Be(order.Products.First().Remarks.Count());
            //rehydrated.Order.Products.First().Remarks.Any(x => x == order.Products.First().Remarks.First()).Should().BeTrue();
            //rehydrated.Order.Products.First().Remarks.Any(x => x == order.Products.First().Remarks.Last()).Should().BeTrue();
            //rehydrated.Order.Products.Last().Remarks.Any(x => x == order.Products.Last().Remarks.First()).Should().BeTrue();
            //rehydrated.Order.Products.Last().Remarks.Any(x => x == order.Products.Last().Remarks.Last()).Should().BeTrue();
            rehydrated.Order.Operation.Should().NotBeNull();
            rehydrated.Order.Operation.ExtraActivities.Should().NotBeNull();
            rehydrated.Order.Operation.ExtraActivities.Count().Should().Be(order.Operation.ExtraActivities.Count());
            rehydrated.Order.Operation.ExtraActivities.Any(x => x.Code == order.Operation.ExtraActivities.First().Code).Should().BeTrue();
            rehydrated.Order.Operation.ExtraActivities.Any(x => x.Code == order.Operation.ExtraActivities.Last().Code).Should().BeTrue();
            rehydrated.Order.Operation.OperationalRemarks.Should().NotBeNull();
            rehydrated.Order.Operation.OperationalRemarks.Count().Should().Be(order.Operation.OperationalRemarks.Count());
            rehydrated.Order.Operation.OperationalRemarks.Any(x => x == order.Operation.OperationalRemarks.First()).Should().BeTrue();
            rehydrated.Order.Operation.OperationalRemarks.Any(x => x == order.Operation.OperationalRemarks.Last()).Should().BeTrue();
        }
    }
}
