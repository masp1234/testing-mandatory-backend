// File: PhoneNumbersControllerTests.cs

using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using testing_mandatory_backend.Controllers;
using testing_mandatory_backend.Services;
using Newtonsoft.Json;

namespace testing_mandatory_backend.Tests
{
    public class PhoneNumbersControllerTests
    {
        private readonly Mock<ILogger<PhoneNumbersController>> _loggerMock;
        private readonly Mock<IPhoneNumberGenerator> _phoneNumberGeneratorMock;
        private readonly PhoneNumbersController _controller;

        public PhoneNumbersControllerTests()
        {
            _loggerMock = new Mock<ILogger<PhoneNumbersController>>();
            _phoneNumberGeneratorMock = new Mock<IPhoneNumberGenerator>();
            _controller = new PhoneNumbersController(_loggerMock.Object, _phoneNumberGeneratorMock.Object);
        }

        [Fact]
        public void GeneratePhoneNumber_ValidPrefix_ReturnsOkResult()
        {
            // Arrange
            var prefix = "31";
            var expectedPhoneNumber = "31123456";
            _phoneNumberGeneratorMock.Setup(generator => generator.GeneratePhoneNumber(prefix)).Returns(expectedPhoneNumber);

            // Act
            var result = _controller.GeneratePhoneNumber(prefix) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);

            dynamic response = result.Value;
            Assert.Equal(expectedPhoneNumber, (string)response.PhoneNumber);
        }

        [Fact]
        public void GeneratePhoneNumber_InvalidPrefix_ReturnsBadRequestResult()
        {
            // Arrange
            var prefix = "abc";
            var expectedErrorMessage = "Invalid prefix for phone number generation.";
            _phoneNumberGeneratorMock.Setup(generator => generator.GeneratePhoneNumber(prefix))
                                      .Throws(new ArgumentException(expectedErrorMessage));

            // Act
            var response = _controller.GeneratePhoneNumber(prefix) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal(expectedErrorMessage, response.Value);
        }

        [Fact]
        public void GenerateBulkPhoneNumbers_ValidCount_ReturnsOkResult()
        {
            // Arrange
            var count = 5;
            var expectedPhoneNumbers = new List<string> { "31123456", "31234567", "31345678", "31456789", "31567890" };
            _phoneNumberGeneratorMock.Setup(generator => generator.GenerateBulkPhoneNumbers(count)).Returns(expectedPhoneNumbers);

            // Act
            var response = _controller.GenerateBulkPhoneNumbers(count) as OkObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(200, response.StatusCode);

            dynamic responseValue = response.Value;
            Assert.NotNull(responseValue.PhoneNumbers);
            var phoneNumbers = (List<string>)responseValue.PhoneNumbers;
            Assert.Equal(count, phoneNumbers.Count);
            Assert.Equal(expectedPhoneNumbers, phoneNumbers);
        }

        [Fact]
        public void GenerateBulkPhoneNumbers_InvalidCount_ReturnsBadRequestResult()
        {
            // Arrange
            int invalidCount = -1;

            // Act
            var response = _controller.GenerateBulkPhoneNumbers(invalidCount) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(response);
            Assert.Equal(400, response.StatusCode);
            Assert.Equal("Count must be at least 1.", response.Value);
        }

        // Example of addressing xUnit1012 warnings in other tests
        [Fact]
        public void SomeOtherTest_ValidInputs_ReturnsExpectedResult()
        {
            // Arrange
            string townName = "SampleTown";
            string postalCode = "12345";

            // Act
            // Call the method under test with valid inputs

            // Assert
            // Verify the expected outcome
        }
    }
}