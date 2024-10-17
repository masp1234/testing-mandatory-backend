using Google.Protobuf.WellKnownTypes;
using Moq;
using testing_mandatory_backend.Models;
using testing_mandatory_backend.Services;

public class PersonDataServiceFixture
{
    public PersonDataService PersonDataService { get; }

    public PersonDataServiceFixture()
    { 
        var mockPhoneNumberGenerator = new Mock<IPhoneNumberGenerator>();
        mockPhoneNumberGenerator.Setup(generator => generator.GeneratePhoneNumber(It.IsAny<string>()))
            .Returns("4542022928");

        var mockBirthdayGenerator = new Mock<IBirthdayGenerator>();
        mockBirthdayGenerator.Setup(generator => generator.GenerateRandomBirthday())
            .Returns(new DateTime(1990, 1, 1));

        var mockFakeAddressGenerator = new Mock<IFakeAddressGenerator>();
        mockFakeAddressGenerator.Setup(generator => generator.GenerateFakeAddress())
            .Returns(new FakeAddress("A", "B", "C", "D", "12345", "Sample Town"));

        var mockNameAndGenderGenerator = new Mock<INameAndGenderGenerator>();
        mockNameAndGenderGenerator.Setup(generator => generator.GenerateNameAndGender())
            .Returns(new NameAndGender("John", "Doe", "Male"));

        var mockCprGenerator = new Mock<ICprGenerator>();
        mockCprGenerator.Setup(generator => generator
            .GenerateCprWithBirthdayAndGender(It.IsAny<DateTime>(), It.IsAny<string>()))
            .Returns("210298-2704");

        mockCprGenerator.Setup(generator => generator
            .GenerateRandomCpr(It.IsAny<DateTime>()))
            .Returns("210298-2704");


        PersonDataService = new(mockBirthdayGenerator.Object,
                                mockPhoneNumberGenerator.Object,
                                mockNameAndGenderGenerator.Object,
                                mockFakeAddressGenerator.Object,
                                mockCprGenerator.Object);
    }
}
