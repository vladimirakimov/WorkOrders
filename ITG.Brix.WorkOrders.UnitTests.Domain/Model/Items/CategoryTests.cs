using FluentAssertions;
using ITG.Brix.WorkOrders.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.Domain
{
    [TestClass]
    public class CategoryTests
    {
        [TestMethod]
        public void CreateCategoryShouldSucceed()
        {
            // Arrange
            var generalGroup = GeneralGroup.None;
            var teamFilterGroup = TeamFilterGroup.None;
            var productGroup = ProductGroup.None;

            // Act
            Action ctor = () => { new Category(generalGroup, teamFilterGroup, productGroup); };

            // Assert
            ctor.Should().NotThrow();
        }

        [TestMethod]
        public void CreateCategoryShouldSetCorrectlyProperties()
        {
            // Arrange
            var generalGroup = GeneralGroup.None;
            var teamFilterGroup = TeamFilterGroup.None;
            var productGroup = ProductGroup.None;

            // Act
            var category = new Category(generalGroup, teamFilterGroup, productGroup);

            // Assert
            category.General.Should().Be(generalGroup);
            category.TeamFilter.Should().Be(teamFilterGroup);
            category.Product.Should().Be(productGroup);
        }
    }
}
