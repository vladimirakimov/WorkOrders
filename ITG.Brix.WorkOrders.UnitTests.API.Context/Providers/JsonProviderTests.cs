using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Providers;
using ITG.Brix.WorkOrders.API.Context.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Providers
{
    [TestClass]
    public class JsonProviderTests
    {
        [TestMethod]
        public void GenerateShouldBeInRange()
        {
            // Arrange
            IJsonProvider jsonProvider = new JsonProvider();
            var json = @"{
                                    ""firstName"" : ""TheFirstName"",
                                    ""lastName"" : ""TheLastName""
                                 }";

            // Act
            var result = jsonProvider.ToDictionary(json);

            // Assert
            result.Should().NotBeNull();
            result.Keys.Count.Should().Be(2);
            result["firstName"].Should().Be("TheFirstName");
            result["lastName"].Should().Be("TheLastName");
        }
    }
}
