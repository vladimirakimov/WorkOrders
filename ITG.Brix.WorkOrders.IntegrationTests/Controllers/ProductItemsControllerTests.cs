using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Models;
using ITG.Brix.WorkOrders.IntegrationTests.Bases;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Controllers
{
    [TestClass]
    [TestCategory("Integration")]
    public class ProductItemsControllerTests
    {
        private HttpClient _client;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            ControllerTestsHelper.InitServer();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
        }

        [TestInitialize]
        public void TestInitialize()
        {
            _client = ControllerTestsHelper.GetClient();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _client.Dispose();
        }

        [TestMethod]
        public async Task ListAllShouldSucceed()
        {
            // Arrange
            var apiVersion = "1.0";

            // Act
            var response = await _client.GetAsync(string.Format("api/productitems?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var productItemsModel = JsonConvert.DeserializeObject<ProductItemsModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            productItemsModel.Value.Should().NotBeNull();
            productItemsModel.NextLink.Should().BeNull();
        }
    }
}
