using Moq;
using testing_mandatory_backend.Models;
using testing_mandatory_backend.Services;
using testing_mandatory_backendTests.Util;
using Xunit;

namespace testing_mandatory_backendTests.Unit
{
    public class PersonDataServiceTests: IClassFixture<PersonDataServiceFixture>
    {
        private readonly PersonDataServiceFixture _fixture;
        public PersonDataServiceTests(PersonDataServiceFixture personDataServiceFixture) {
            _fixture = personDataServiceFixture;
        }
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(101)]
        public void GenerateBulkPersonData_ShouldThrowException_WhenCalled_WithInvalidAmount(int amount)
        {
            var exception = Assert.Throws<ArgumentException>(() => _fixture.PersonDataService.GenerateBulkPersonData(amount));
            Assert.Contains("between 2 and 100", exception.Message);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(99)]
        [InlineData(100)]
        public void GenerateBulkPersonData_ShouldReturnCorrectAmountOfData_WhenCalled_WithValidAmount(int amount)
        {
            List<PersonData> peopleData = _fixture.PersonDataService.GenerateBulkPersonData(amount);

            Assert.Equal(amount, peopleData.Count);
        }

        [Fact]
        public void GenerateBulkPersonData_ShouldReturn_ValidPersonData()
        {
            List<PersonData> peopleData = _fixture.PersonDataService.GenerateBulkPersonData(5);
            Assert.Equal(5, peopleData.Count());
            peopleData.ForEach(personData =>
            {
                PersonDataValidator.CheckForNullValues(personData);
            });
        }

        [Fact]
        public void CreatePersonData_ShouldReturn_ValidPersonData()
        {
            PersonData personData = _fixture.PersonDataService.CreatePersonData();
            PersonDataValidator.CheckForNullValues(personData);
        }

        [Fact]
        public void CreatePersonData_ShouldThrowException_When_AGeneratorFails()
        {
            var mockPhoneNumberGenerator = new Mock<IPhoneNumberGenerator>();
            mockPhoneNumberGenerator.Setup(generator => generator.GeneratePhoneNumber(It.IsAny<string>()))
                .Returns("4542022928");

            var mockBirthdayGenerator = new Mock<IBirthdayGenerator>();
            mockBirthdayGenerator.Setup(generator => generator.GenerateRandomBirthday())
                .Returns(new DateTime(1990, 1, 1));

            var mockFakeAddressGenerator = new Mock<IFakeAddressGenerator>();
            mockFakeAddressGenerator.Setup(generator => generator.GenerateFakeAddress())
                .Throws(new Exception("No postalcodes were found."));

            var mockNameAndGenderGenerator = new Mock<INameAndGenderGenerator>();
            mockNameAndGenderGenerator.Setup(generator => generator.GenerateNameAndGender())
                .Returns(new NameAndGender("John", "Doe", "Male"));

            PersonDataService personDataService = new(mockBirthdayGenerator.Object,
                                    mockPhoneNumberGenerator.Object,
                                    mockNameAndGenderGenerator.Object,
                                    mockFakeAddressGenerator.Object);

            var exception = Assert.Throws<InvalidOperationException>(() => personDataService.CreatePersonData());
            Assert.Equal("Failed to generate person data.", exception.Message);
        }
        }
        }
