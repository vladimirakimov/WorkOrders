using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators;
using ITG.Brix.WorkOrders.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class CreateWorkOrderCommandValidatorTests
    {
        private CreateWorkOrderCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new CreateWorkOrderCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var userCreated = "anyUserCreated";
            var operation = "AnyOperation";
            var operationalDepartment = "any";
            var site = "any";

            var command = new CreateWorkOrderCommand(userCreated, site, operation, operationalDepartment);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveSiteMandatoryWhenSiteIsNull()
        {
            var userCreated = "anyUserCreated";
            var operation = "AnyOperation";
            var operationalDepartment = "any";
            string site = null;

            var command = new CreateWorkOrderCommand(userCreated, site, operation, operationalDepartment);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Site") && a.ErrorMessage.Contains(ValidationFailures.SiteMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveOperationMandatoryWhenOperationIsNull()
        {
            var userCreated = "anyUserCreated";
            string operation = null;
            var operationalDepartment = "any";
            var site = "any";

            var command = new CreateWorkOrderCommand(userCreated, site, operation, operationalDepartment);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Operation") && a.ErrorMessage.Contains(ValidationFailures.OperationMandatory));

            // Assert
            exists.Should().BeTrue();
        }

        [TestMethod]
        public void ShouldHaveUserMandatoryWhenUserCreatedIsNull()
        {
            string userCreated = null;
            var operation = "AnyOperation";
            var operationalDepartment = "any";
            var site = "any";

            var command = new CreateWorkOrderCommand(userCreated, site, operation, operationalDepartment);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("UserCreated") && a.ErrorMessage.Contains(ValidationFailures.UserCreatedMandatory));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
