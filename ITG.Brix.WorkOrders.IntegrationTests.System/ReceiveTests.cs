using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Services;
using ITG.Brix.WorkOrders.Application.Services.Acls;
using ITG.Brix.WorkOrders.Application.Services.Acls.Impl;
using ITG.Brix.WorkOrders.Application.Services.Impl;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.System
{
    [TestClass]
    [TestCategory("Integration")]
    public class ReceiveTests
    {
        [TestMethod]
        [Ignore("Receive: manual trigger required")]
        public async Task GetOrderShouldSucceed()
        {
            // Arrange
            var jsonBody = new
            {
                source = "BKAL33+KBT T",
                relationType = "Inbound",
                transportNo = "784037",
                operationGroup = "UNLOADING SVEN",
                operation = "UNLOADING SVEN"
            };
            var jsonBodyAsString = JsonConvert.SerializeObject(jsonBody);

            IPlatoOrderProvider _platoOrderProvider = new PlatoOrderProvider();

            IDateTimeProvider dateTimeProvider = new DateTimeProvider();
            ITypeConverterProvider typeConverterProvider = new TypeConverterProvider();
            IPlatoDataAcl platoDataAcl = new PlatoDataAcl(dateTimeProvider);
            IDomainConverter domainConverter = new DomainConverter(platoDataAcl, typeConverterProvider);

            HttpResponseMessage response = null;

            // Act
            using (HttpClient client = new HttpClient())
            {
                response = await client.PostAsync("http://s-be-ki-btst1/ECC/BTSHTTPReceive.dll", new StringContent(jsonBodyAsString, Encoding.UTF8, "application/json"));
            }
            var responseBody = await response.Content.ReadAsStringAsync();
            var platoOrderFull = _platoOrderProvider.GetPlatoOrderFull(responseBody);
            var order = domainConverter.ToOrder(platoOrderFull.Transport);


            // Assert
            order.Should().NotBeNull();
        }
    }
}
