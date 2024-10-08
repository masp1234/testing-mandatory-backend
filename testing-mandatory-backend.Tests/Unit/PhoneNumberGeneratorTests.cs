using testing_mandatory_backend.Services;  // Import the namespace containing PhoneNumberGenerator
using Xunit;

namespace testing_mandatory_backend.Tests
{
    public class PhoneNumberGeneratorTests
    {
        private readonly PhoneNumberGenerator _generator;

        public PhoneNumberGeneratorTests()
        {
            _generator = new PhoneNumberGenerator();
        }

        [Theory]
        [InlineData("2")]
        [InlineData("30")]
        [InlineData("342")]
        [InlineData("40")]
        [InlineData("356")]
        public void GeneratePhoneNumber_ValidPrefixes_ValidLengthAndPrefix(string prefix)
        {
            // Act
            var result = _generator.GeneratePhoneNumber(prefix);

            // Assert
            Assert.Equal(8, result.Length); // Check that the length is correct
            Assert.StartsWith(prefix, result); // Check that the number starts with the expected prefix
            AssertValidPhoneNumber(result);
        }

        [Theory]
        [InlineData("00")]
        [InlineData("1")]
        [InlineData("4")]
        [InlineData("9")]
        [InlineData("3A")]
        public void GeneratePhoneNumber_InvalidPrefix_ShouldThrowException(string invalidPrefix)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _generator.GeneratePhoneNumber(invalidPrefix));
            Assert.Equal("Invalid prefix for phone number generation", ex.Message);
        }

        [Fact]
        public void GeneratePhoneNumber_LengthValidation_ShouldBeEightDigits()
        {
            // Act
            var result = _generator.GeneratePhoneNumber("2");

            // Assert
            Assert.Equal(8, result.Length);
        }

        [Fact]
        public void GeneratePhoneNumber_BulkGeneration_UniqueNumbers()
        {
            // Act
            var results = _generator.GenerateBulkPhoneNumbers(100);

            // Assert
            Assert.Equal(100, results.Count);
            Assert.Equal(100, results.Distinct().Count());
        }

        [Theory]
        [InlineData(100)]
        [InlineData(500)]
        [InlineData(1000)]
        [InlineData(5000)]
        public void GeneratePhoneNumber_BulkGeneration_PerformanceTest(int count)
        {
            // Act
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var results = _generator.GenerateBulkPhoneNumbers(count);
            watch.Stop();

            // Assert
            Assert.Equal(count, results.Count);
            Assert.InRange(watch.ElapsedMilliseconds, 0, 1000); // Assert performance within 1 second
            Assert.Equal(count, results.Distinct().Count());
        }

        [Theory]
        [InlineData("29")]
        [InlineData("350")]
        public void GeneratePhoneNumber_InvalidBoundaryPrefixes_ShouldThrowException(string invalidPrefix)
        {
            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _generator.GeneratePhoneNumber(invalidPrefix));
            Assert.Equal("Invalid prefix for phone number generation", ex.Message);
        }

        [Fact]
        public void GeneratePhoneNumber_RandomPrefixWithinValidRange_ShouldGenerateValidPhoneNumbers()
        {
            // Act
            var randomPrefix = GetRandomValidPrefix();
            var result = _generator.GeneratePhoneNumber(randomPrefix);

            // Assert
            Assert.Equal(8, result.Length);
            Assert.StartsWith(randomPrefix, result);
            AssertValidPhoneNumber(result);
        }

        private string GetRandomValidPrefix()
        {
            var validPrefixes = new List<string> { "2", "30", "31", "342", "344", "357", "40", "571", "785" };
            var random = new Random();
            return validPrefixes[random.Next(validPrefixes.Count)];
        }

        private void AssertValidPhoneNumber(string phoneNumber)
        {
            // Helper method to validate a phone number's format and validity
            Assert.All(phoneNumber, c => Assert.True(char.IsDigit(c), $"The character '{c}' is not a digit.")); // Ensure all characters are digits
            Assert.True(phoneNumber.Distinct().Count() > 1, "Phone number contains repeated identical digits, which may be invalid");
        }
    }
}
