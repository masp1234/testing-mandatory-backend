using testing_mandatory_backend.Models;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using testing_mandatory_backendTests.Util;
using Xunit;

namespace testing_mandatory_backendTests.Integration
{
    [Trait("Category", "Integration")]
    [Collection("Sequential")]
    /* 
        Big bang approach - connecting this component and 
        all the downstream dependencies and testing them in one go
    */

    public class PersonDataServiceIntegrationTests : IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _testDatabaseFixture;
        private readonly PersonDataService _personDataService;
        public PersonDataServiceIntegrationTests(
            TestDatabaseFixture testDatabaseFixture
            )
        {
            _testDatabaseFixture = testDatabaseFixture;
            _testDatabaseFixture.CreateNewConnection();

            NameAndGenderRepository nameAndGenderRepository = new();
            PostalCodeRepository postalCodeRepository = new(_testDatabaseFixture.Connection);

            IBirthdayGenerator birthdayGenerator = new BirthdayGenerator();
            IPhoneNumberGenerator phoneNumberGenerator = new PhoneNumberGenerator();
            INameAndGenderGenerator nameAndGenderGenerator = new NameAndGenderGenerator(nameAndGenderRepository);
            IFakeAddressGenerator fakeAddressGenerator = new FakeAddressGenerator(postalCodeRepository);
            ICprGenerator cprGenerator = new CprGenerator();

            _personDataService = new(
                birthdayGenerator,
                phoneNumberGenerator,
                nameAndGenderGenerator,
                fakeAddressGenerator,
                cprGenerator
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
            // Successfully failing because the database has no data
            var exception = Assert.Throws<Exception>(() => _personDataService.GenerateBulkPersonData(2));
            Assert.Contains("Failed to generate person data.", exception.Message);
        }

        [Fact]
        public void CreatePhoneNumber_ShouldReturn_ValidData()
        {
            string phoneNumber = _personDataService.CreatePhoneNumber();
            Assert.False(string.IsNullOrEmpty(phoneNumber));
        }

        [Fact]
        public void CreateNameAndGender_ShouldReturn_ValidData()
        {
            NameAndGender nameAndGender = _personDataService.CreateNameAndGender();
            Assert.False(string.IsNullOrEmpty(nameAndGender.Name));
            Assert.False(string.IsNullOrEmpty(nameAndGender.Surname));
            Assert.False(string.IsNullOrEmpty(nameAndGender.Gender));
        }

        [Fact]
        public void CreateNameAndGenderAndBirthDate()
        {
            NameGenderAndBirthDate nameGenderAndBirthDate = _personDataService
                .CreateNameAndGenderAndBirthDate();

            Assert.False(string.IsNullOrEmpty(nameGenderAndBirthDate.Name));
            Assert.False(string.IsNullOrEmpty(nameGenderAndBirthDate.Surname));
            Assert.False(string.IsNullOrEmpty(nameGenderAndBirthDate.Gender));
            Assert.NotEqual(DateTime.MinValue, nameGenderAndBirthDate.BirthDate);
        }

        [Fact]
        public void CreateAddress_ShouldReturn_ValidData()
        {
            _testDatabaseFixture.SeedDatabase(("1234", "Town"), ("9876", "AnotherTown"));
            FakeAddress address = _personDataService.CreateAddress();

            Assert.False(string.IsNullOrEmpty(address.Number));
            Assert.False(string.IsNullOrEmpty(address.TownName));
            Assert.False(string.IsNullOrEmpty(address.Floor));
            Assert.False(string.IsNullOrEmpty(address.Door));
        }

        [Fact]
        public void CreateAddress_ShouldThrowException_WhenNoAccessToPostalCodes()
        {
            // Successfully failing because the database has no data
            var exception = Assert.Throws<Exception>(() => _personDataService.CreateAddress());
            Assert.Contains("Could not create a fake address.", exception.Message);
        }

        [Fact]
        public void CreateCPR_ShouldReturn_ValidData()
        {
            string cpr = _personDataService.CreateCPR();
            Assert.False(string.IsNullOrEmpty(cpr));
        }

        [Fact]
        public void CreateCprNameAndGender_ShouldReturn_ValidData()
        {
            CprNameAndGender cprNameAndGender = _personDataService.CreateCprNameAndGender();

            Assert.False(string.IsNullOrEmpty(cprNameAndGender.Gender));
            Assert.False(string.IsNullOrEmpty(cprNameAndGender.Name));
            Assert.False(string.IsNullOrEmpty(cprNameAndGender.Surname));
            Assert.False(string.IsNullOrEmpty(cprNameAndGender.CPR));
        }

        [Fact]
        public void CreateCprNameGenderAndBirthDate_ShouldReturn_ValidData()
        {
            CprNameGenderAndBirthDate cprNameGenderAndBirthDate = _personDataService.CreateCprNameGenderAndBirthDate();

            Assert.False(string.IsNullOrEmpty(cprNameGenderAndBirthDate.Gender));
            Assert.False(string.IsNullOrEmpty(cprNameGenderAndBirthDate.Name));
            Assert.False(string.IsNullOrEmpty(cprNameGenderAndBirthDate.Surname));
            Assert.False(string.IsNullOrEmpty(cprNameGenderAndBirthDate.CPR));
            Assert.NotEqual(DateTime.MinValue, cprNameGenderAndBirthDate.BirthDate);
        }
    }
}