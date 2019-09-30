using FluentAssertions;
using ITG.Brix.WorkOrders.Infrastructure.Providers;
using ITG.Brix.WorkOrders.Infrastructure.Providers.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace ITG.Brix.WorkOrders.UnitTests.Infrastructure.Providers
{
    [TestClass]
    public class FilterProviderTests
    {
        [DataTestMethod]
        [FilterReplaceTestData]
        public void ReplaceShouldReturnCorrectResult(string filter, Dictionary<string, string> replacements, string expected)
        {
            // Arrange
            IFilterProvider filterProvider = new FilterProvider();

            // Act
            var result = filterProvider.Replace(filter, replacements);

            // Assert
            result.Should().Be(expected);
        }
    }

    internal class FilterReplaceTestData : Attribute, ITestDataSource
    {
        public IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            return new List<object[]> {
                new object[] { "productName eq 'HGT'", new Dictionary<string, string>() { { "productName", "Product/Code" } }, "Product/Code eq 'HGT'" },
                new object[] { "product eq 'Hero'", new Dictionary<string, string>() { { "product", "Product/Code" } }, "Product/Code eq 'Hero'" },
                new object[] { null, new Dictionary<string, string>() { { "productName", "Product/Code" } }, null },
                new object[] { "", new Dictionary<string, string>() { { "productName", "Product/Code" } }, null },
                new object[] { "  ", new Dictionary<string, string>() { { "productName", "Product/Code" } }, null },
            };
        }

        public string GetDisplayName(MethodInfo methodInfo, object[] data)
        {
            if (data != null)
            {
                return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", methodInfo.Name, string.Join(",", data));
            }
            return null;
        }
    }
}
