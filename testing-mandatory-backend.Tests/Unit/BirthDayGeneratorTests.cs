using System;
using System.Collections.Generic;
using testing_mandatory_backend.Services;
using testing_mandatory_backend.Tests.Fixtures;
using Xunit;

namespace testing_mandatory_backend.Tests {
    [Trait("Category", "Unit")]
    public class BirthdayGeneratorTests : IClassFixture<BirthdayGeneratorFixture> {
        private readonly BirthdayGeneratorFixture _fixture;

        public BirthdayGeneratorTests(BirthdayGeneratorFixture fixture) { // Uses fixture from BirthdayGeneratorFixture, and applies it in all tests.
            _fixture = fixture;
        }

        [Fact]
        public void GenerateRandomBirthday_ShouldReturnDateWithinValidRange() {
            // Arrange
            DateTime today = DateTime.Today;
            DateTime earliestDate = today.AddYears(-100);
            DateTime latestDate = today.AddYears(-18);

            // Act & Assert
            foreach (var randomBirthday in _fixture.RandomBirthdays) {
                Assert.InRange(randomBirthday, earliestDate, latestDate);
            }
        }

        [Fact]
        public void GenerateRandomBirthday_ShouldNotReturnFutureDate() {
            // Act & Assert
            foreach (var randomBirthday in _fixture.RandomBirthdays) {
                Assert.True(randomBirthday <= DateTime.Today);
            }
        }

        [Fact]
        public void GenerateRandomBirthday_ShouldNotReturnDateMoreThan100YearsAgo() {
            // Act & Assert
            foreach (var randomBirthday in _fixture.RandomBirthdays) {
                Assert.True(randomBirthday >= DateTime.Today.AddYears(-100));
            }
        }

        [Fact]
        public void GenerateRandomBirthday_ShouldNotReturnDateLessThan18YearsAgo() {
            // Act & Assert
            foreach (var randomBirthday in _fixture.RandomBirthdays) {
                Assert.True(randomBirthday <= DateTime.Today.AddYears(-18));
            }
        }
    }
}