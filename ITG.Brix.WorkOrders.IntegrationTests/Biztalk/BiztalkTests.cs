using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITG.Brix.WorkOrders.IntegrationTests.Biztalk
{
    [TestClass]
    [TestCategory("Integration")]
    public class BiztalkTests
    {

        [TestMethod]
        [Ignore("Biztalk: manual trigger required")]
        public async Task GetOrderShouldSucceed()
        {
            // Arrange
            var body = new
            {
                source = "BKAL33+KBT T",
                relationType = "Inbound",
                transportNo = "781415",
                operation = "Unload into warehouse"
            };
            var jsonBody = JsonConvert.SerializeObject(body);
            HttpResponseMessage response = null;

            // Act
            using (HttpClient client = new HttpClient())
            {
                response = await client.PostAsync("http://s-be-ki-btst1/ECC/BTSHTTPReceive.dll", new StringContent(jsonBody, Encoding.UTF8, "application/json"));
            }
            var responseBody = await response.Content.ReadAsStringAsync();

            // Assert
            response.Should().NotBeNull();
            response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
            responseBody.Should().NotBeNull();
        }
    }
}
