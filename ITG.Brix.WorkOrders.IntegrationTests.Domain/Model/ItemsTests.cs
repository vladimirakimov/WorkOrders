using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using System.Text.RegularExpressions;

namespace ITG.Brix.WorkOrders.IntegrationTests.Domain.Model
{
    [TestClass]
    public class ItemsTests
    {
        [TestMethod]
        public void ItemAndFilterKeysCountShouldBeEqual()
        {
            // Arrange
            var itemKeyCount = ItemKey.List().Count();
            var filterKeyCount = FilterKey.List().Count();

            // Act
            var result = itemKeyCount == filterKeyCount;

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void FilterKeysShouldBeComposedFromLowercaseLettersOrDigitsOrDashSymbols()
        {
            // Arrange
            var because = string.Empty;
            var regex = new Regex(@"^[0-9a-z\-]+$");
            var filterKeyValues = FilterKey.List().Select(x => x.Value).ToList();

            // Act

            var result = true;
            foreach (var value in filterKeyValues)
            {
                if (!regex.IsMatch(value))
                {
                    because = string.Format("'{0}' should be in correct format", value);
                    result = false;
                    break;
                }
            }

            // Assert
            result.Should().BeTrue(because);
        }

        [TestMethod]
        public void ItemKeysShouldBeComposedFromLowercaseLettersOrDigitsOrUnderlineSymbols()
        {
            // Arrange
            var because = string.Empty;
            var regex = new Regex(@"^(#)+[0-9a-z_]+$");
            var itemKeyValues = ItemKey.List().Select(x => x.Value).ToList();

            // Act
            var result = true;
            foreach (var value in itemKeyValues)
            {
                if (!regex.IsMatch(value))
                {
                    because = string.Format("'{0}' should be in correct format", value);
                    result = false;
                    break;
                }
            }

            // Assert
            result.Should().BeTrue(because);
        }

    }
}
