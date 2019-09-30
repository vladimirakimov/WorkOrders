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
    public class PropertiesControllerTests
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
            DatabaseHelper.Init("WorkOrders");
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

            await ControllerHelper.CreateWorkOrder("anyUser");

            // Act
            var response = await _client.GetAsync(string.Format("api/properties?api-version={0}", apiVersion));
            var responseBody = await response.Content.ReadAsStringAsync();
            var propertiesModel = JsonConvert.DeserializeObject<PropertiesModel>(responseBody);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            propertiesModel.Value.Should().NotBeNull();
            propertiesModel.NextLink.Should().BeNull();
        }
    }
}
