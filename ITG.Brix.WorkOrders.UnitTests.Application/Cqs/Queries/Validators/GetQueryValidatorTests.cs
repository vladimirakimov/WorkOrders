using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Queries.Validators;
using ITG.Brix.WorkOrders.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Queries.Validators
{
    [TestClass]
    public class GetQueryValidatorTests
    {
        private GetWorkOrderQueryValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new GetWorkOrderQueryValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var query = new GetWorkOrderQuery(id: Guid.NewGuid());

            // Act
            var validationResult = _validator.Validate(query);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveUserNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var query = new GetWorkOrderQuery(id: Guid.Empty);

            // Act
            var validationResult = _validator.Validate(query);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.WorkOrderNotFound));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
