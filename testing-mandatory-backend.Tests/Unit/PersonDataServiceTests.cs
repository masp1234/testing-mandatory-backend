using System.Reflection;
using testing_mandatory_backend.Models;
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
        }
    }
