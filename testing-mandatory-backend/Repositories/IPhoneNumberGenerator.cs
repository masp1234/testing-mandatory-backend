using System;
// File: Services/IPhoneNumberGenerator.cs

using System.Collections.Generic;

// File: Services/IPhoneNumberGenerator.cs

using System.Collections.Generic;

namespace testing_mandatory_backend.Services
{
    public interface IPhoneNumberGenerator
    {
        /// <summary>
        /// Generates a phone number with the specified prefix.
        /// </summary>
        /// <param name="prefix">The prefix for the phone number.</param>
        /// <returns>A generated phone number with the prefix.</returns>
        string GeneratePhoneNumber(string prefix);
        
        /// <summary>
        /// Generates a phone number without any prefix.
        /// </summary>
        /// <returns>A generated phone number without a prefix.</returns>
        string GeneratePhoneNumberWithoutPrefix();
        
        /// <summary>
        /// Generates a bulk list of phone numbers.
        /// </summary>
        /// <param name="count">The number of phone numbers to generate.</param>
        /// <returns>A list of generated phone numbers.</returns>
        List<string> GenerateBulkPhoneNumbers(int count);
        
        /// <summary>
        /// Validates if the provided prefix is valid for phone number generation.
        /// </summary>
        /// <param name="prefix">The prefix to validate.</param>
        /// <returns>True if the prefix is valid, false otherwise.</returns>
        bool IsValidPrefix(string prefix);
    }
}