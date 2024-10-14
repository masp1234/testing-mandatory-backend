using Moq;
using testing_mandatory_backend.Services;
using testing_mandatory_backend.Models;
using Xunit;

namespace testing_mandatory_backendTests.Unit
{
    public class PersonDataServiceTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(1)]
        [InlineData(101)]
        public void GenerateBulkPersonData_ShouldThrowException_WhenCalled_WithInvalidAmount(int amount)
        {

            var mockPhoneNumberGenerator = new Mock<IPhoneNumberGenerator>();
            var mockBirthdayGenerator = new Mock<IBirthdayGenerator>();
            var mockFakeAddressGenerator = new Mock<IFakeAddressGenerator>();
            var mockNameAndGenderGenerator = new Mock<INameAndGenderGenerator>();
            PersonDataService sut = new(mockBirthdayGenerator.Object,
                                        mockPhoneNumberGenerator.Object,
                                        mockNameAndGenderGenerator.Object,
                                        mockFakeAddressGenerator.Object);

            var exception = Assert.Throws<ArgumentException>(() =>  sut.GenerateBulkPersonData(amount));
            Assert.Contains("between 2 and 100", exception.Message);
        }

        [Theory]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(99)]
        [InlineData(100)]
        public void GenerateBulkPersonData_ShouldReturnCorrectAmountOfData_WhenCalled_WithValidAmount(int amount)
        {
            var mockPhoneNumberGenerator = new Mock<IPhoneNumberGenerator>();
            var mockBirthdayGenerator = new Mock<IBirthdayGenerator>();
            var mockFakeAddressGenerator = new Mock<IFakeAddressGenerator>();
            var mockNameAndGenderGenerator = new Mock<INameAndGenderGenerator>();
            PersonDataService sut = new(mockBirthdayGenerator.Object,
                                        mockPhoneNumberGenerator.Object,
                                        mockNameAndGenderGenerator.Object,
                                        mockFakeAddressGenerator.Object);
            List<PersonData> peopleData = sut.GenerateBulkPersonData(amount);

            Assert.Equal(amount, peopleData.Count);
        }


    }
}
