using FluentAssertions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Definitions;
using ITG.Brix.WorkOrders.Application.Cqs.Commands.Validators;
using ITG.Brix.WorkOrders.Application.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace ITG.Brix.WorkOrders.UnitTests.Application.Cqs.Commands.Validators
{
    [TestClass]
    public class DeleteWorkOrderCommandValidatorTests
    {
        private DeleteWorkOrderCommandValidator _validator;

        [TestInitialize]
        public void TestInitialize()
        {
            _validator = new DeleteWorkOrderCommandValidator();
        }

        [TestMethod]
        public void ShouldContainNoErrors()
        {
            // Arrange
            var command = new DeleteWorkOrderCommand(id: Guid.NewGuid(), version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists = validationResult.Errors.Count > 0;

            // Assert
            exists.Should().BeFalse();
        }

        [TestMethod]
        public void ShouldHaveUserNotFoundCustomFailureWhenIdIsGuidEmpty()
        {
            // Arrange
            var command = new DeleteWorkOrderCommand(id: Guid.Empty, version: 0);

            // Act
            var validationResult = _validator.Validate(command);
            var exists =
                validationResult.Errors.Any(
                    a => a.PropertyName.Equals("Id") && a.ErrorMessage.Contains(CustomFailures.WorkOrderNotFound));

            // Assert
            exists.Should().BeTrue();
        }
    }
}
