﻿using FluentAssertions;
using ITG.Brix.WorkOrders.API.Context.Bases;
using ITG.Brix.WorkOrders.API.Context.Services.Requests.Validators.Components.Impl;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace ITG.Brix.WorkOrders.UnitTests.API.Context.Services.Requests.Validators.Components
{
    [TestClass]
    public class RequestComponentValidatorTests
    {
        [TestMethod]
        public void RouteIdShouldReturnNullWhenIdIsValid()
        {
            // Arrange
            var requestComponentValidator = new RequestComponentValidator();
            var id = Guid.NewGuid().ToString();

            // Act
            var result = requestComponentValidator.RouteId(id);

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public void RouteIdShouldReturnValidationResultWhenIdIsNotValid()
        {
            // Arrange
            var requestComponentValidator = new RequestComponentValidator();
            var id = "invalid-id";

            // Act
            var result = requestComponentValidator.RouteId(id);

            // Assert
            result.Should().BeOfType<ValidationResult>();
        }
    }
}
