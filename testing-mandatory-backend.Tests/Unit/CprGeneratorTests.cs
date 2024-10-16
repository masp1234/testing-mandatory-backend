using System;
using Xunit;
using testing_mandatory_backend.Services;
using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Tests
{
    [Trait("Category", "Unit")]
    public class CprGeneratorTests {
        private readonly CprGenerator _cprGenerator;

        public CprGeneratorTests() {
            _cprGenerator = new CprGenerator();
        }

        /*[Fact]
        public void GenerateRandomCpr_ShouldReturnValidCpr() {
            // Act
            string cpr = _cprGenerator.GenerateRandomCpr();

            // Assert
            Assert.Matches
            string datePart = cpr.Substring(0, 6);
            DateTime date = DateTime.ParseExact(datePart, "ddMMyy", null);
            DateTime startDate = DateTime.Today.AddYears(-100);
            DateTime endDate = DateTime.Today.AddYears(-18);

            Assert.InRange(date, startDate, endDate); // Check date range
        }*/

        [Theory]
        [InlineData("male")]
        [InlineData("female")]
        public void GenerateCprWithBirthdayAndGender_ShouldReturnValidCpr(string gender) {

            // Act
            var result = _cprGenerator.GenerateCprWithBirthdayAndGender(gender);
            string cpr = result.cpr;
            Person person = result.randomPerson;
            DateTime birthday = result.randomBirthday;

            // Assert
            Assert.Matches(@"^\d{6}-\d{4}$", cpr); // Check format

            string datePart = cpr.Substring(0, 6);
            DateTime date = DateTime.ParseExact(datePart, "ddMMyy", null);

            Assert.Equal(birthday.Date, date.Date); // Check if date part matches birthday

            int lastDigit = int.Parse(cpr[^1].ToString());
            if (gender.Equals("male", StringComparison.OrdinalIgnoreCase)) {
                Assert.True(lastDigit % 2 != 0); // Check if last digit is odd for males
            }
            else if (gender.Equals("female", StringComparison.OrdinalIgnoreCase)) {
                Assert.True(lastDigit % 2 == 0); // Check if last digit is even for females
            }

            Assert.NotNull(person); // Check if person is not null
            Assert.NotNull(birthday); // Check if birthday is not null
        }
    }
}