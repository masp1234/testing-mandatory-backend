using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations; // Added namespace
using testing_mandatory_backend.Services;

namespace testing_mandatory_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneNumbersController : ControllerBase
    {
        private readonly ILogger<PhoneNumbersController> _logger;
        private readonly IPhoneNumberGenerator _phoneNumberGenerator;

        public PhoneNumbersController(
            ILogger<PhoneNumbersController> logger,
            IPhoneNumberGenerator phoneNumberGenerator)
        {
            _logger = logger;
            _phoneNumberGenerator = phoneNumberGenerator;
        }

        [HttpGet("generate/{prefix?}")]
        public IActionResult GeneratePhoneNumber(string prefix = "")
        {
            try
            {
                string phoneNumber;

                if (string.IsNullOrWhiteSpace(prefix))
                {
                    phoneNumber = _phoneNumberGenerator.GeneratePhoneNumberWithoutPrefix();
                    _logger.LogInformation("Generated phone number '{PhoneNumber}' without prefix.", phoneNumber);
                }
                else
                {
                    if (!_phoneNumberGenerator.IsValidPrefix(prefix))
                    {
                        _logger.LogWarning("Invalid prefix: {Prefix}", prefix);
                        return BadRequest("Invalid prefix for phone number generation.");
                    }

                    phoneNumber = _phoneNumberGenerator.GeneratePhoneNumber(prefix);
                    _logger.LogInformation("Generated phone number '{PhoneNumber}' with prefix '{Prefix}'.", phoneNumber, prefix);
                }

                return Ok(new { PhoneNumber = phoneNumber });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating phone number.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }

        [HttpGet("bulk/{count}")]
        public IActionResult GenerateBulkPhoneNumbers([Range(1, int.MaxValue)] int count)
        {
            if (count < 1)
            {
                _logger.LogWarning("Invalid count: {Count}.", count);
                return BadRequest("Count must be at least 1.");
            }

            try
            {
                var phoneNumbers = _phoneNumberGenerator.GenerateBulkPhoneNumbers(count);
                _logger.LogInformation("Generated {Count} phone numbers.", count);
                return Ok(new { PhoneNumbers = phoneNumbers });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating bulk phone numbers.");
                return StatusCode(500, "An unexpected error occurred.");
            }
        }
    }
}