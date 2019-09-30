using AutoMapper;
using ITG.Brix.WorkOrders.Application.MappingProfiles;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ITG.Brix.WorkOrders.IntegrationTests.Application.MappingProfiles
{
    [TestClass]
    public class DomainProfileTests
    {
        [AssemblyInitialize()]
        public static void ClassInit(TestContext context)
        {
            Mapper.Initialize(m => m.AddProfile<DomainProfile>());
        }

        [TestMethod]
        public void AutoMapperConfigurationShouldBeValid()
        {
            Mapper.AssertConfigurationIsValid();
        }
    }
}
