using testing_mandatory_backend.Models;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using testing_mandatory_backendTests.Util;
using Xunit;

namespace testing_mandatory_backendTests.Integration
{
    [Collection("Sequential")]
    /* 
        Big bang approach - connecting this component and 
        all the downstream dependencies and testing them in one go
     */
    public class PersonDataServiceIntegrationTests: IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _testDatabaseFixture;
        private readonly PersonDataService _personDataService;
        public PersonDataServiceIntegrationTests(
            TestDatabaseFixture testDatabaseFixture
            ) {
            _testDatabaseFixture = testDatabaseFixture;
            _testDatabaseFixture.CreateNewConnection();

            NameAndGenderRepository nameAndGenderRepository = new();
            PostalCodeRepository postalCodeRepository = new(_testDatabaseFixture.Connection);

            IBirthdayGenerator birthdayGenerator = new BirthdayGenerator();
            IPhoneNumberGenerator phoneNumberGenerator = new PhoneNumberGenerator();
            INameAndGenderGenerator nameAndGenderGenerator = new NameAndGenderGenerator(nameAndGenderRepository);
            IFakeAddressGenerator fakeAddressGenerator = new FakeAddressGenerator(postalCodeRepository);

            _personDataService = new(
                birthdayGenerator,
                phoneNumberGenerator,
                nameAndGenderGenerator,
                fakeAddressGenerator
            );

            // Resetting the database between each test (tests that do not seed the database works with an empty database)
            _testDatabaseFixture.ResetDatabase();
        }

        [Fact]
        public void GenerateBulkPersonData_ShouldReturn_ValidPersonData_WithRealGenerators()
        {
            _testDatabaseFixture.SeedDatabase(("1234", "Town"), ("9876", "AnotherTown"));

            int amountOfPeopleData = 5;
           
            List<PersonData> peopleData = _personDataService.GenerateBulkPersonData(amountOfPeopleData);

            Assert.Equal(amountOfPeopleData, peopleData.Count);

            peopleData.ForEach(personData =>
            {
                PersonDataValidator.CheckForNullValues(personData);
            });
        }

        [Fact]
        public void CreatePersonData_ShouldReturn_ValidPersonData()
        {
            _testDatabaseFixture.SeedDatabase(("1234", "Town"), ("9876", "AnotherTown"));

            PersonData personData = _personDataService.CreatePersonData();
            PersonDataValidator.CheckForNullValues(personData);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(101)]
        public void GenerateBulkPersonData_ShouldThrowException_WhenCalled_WithInvalidAmount(int amount)
        {
            var exception = Assert.Throws<ArgumentException>(() => _personDataService.GenerateBulkPersonData(amount));
            Assert.Contains("between 2 and 100", exception.Message);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(99)]
        [InlineData(100)]
        public void GenerateBulkPersonData_ShouldReturnCorrectAmountOfData_WhenCalled_WithValidAmount(int amount)
        {
            _testDatabaseFixture.SeedDatabase(("1234", "Town"), ("9876", "AnotherTown"));

            List<PersonData> peopleData = _personDataService.GenerateBulkPersonData(amount);

            Assert.Equal(amount, peopleData.Count);
        }

        [Fact]
        public void GenerateBulkPersonData_ShouldThrowException_When_AGeneratorFails()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => _personDataService.GenerateBulkPersonData(2));
            Assert.Contains("Failed to generate person data.", exception.Message);
        }


    }
}
