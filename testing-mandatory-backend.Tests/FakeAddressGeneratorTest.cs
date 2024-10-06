using Moq;
using System.Text.RegularExpressions;
using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend.Tests
{
    public class FakeAddressGeneratorTest
    {
        private readonly List<FakeAddress> fakeAddresses = [];
        public FakeAddressGeneratorTest()
        {
            var mockPostalCodeRepository = new Mock<IPostalCodeRepository>();
            mockPostalCodeRepository.Setup(repo => repo.GetPostalCodesAndTowns())
                .Returns([("1234", "TestTown")]);

            FakeAddressGenerator generator = new(mockPostalCodeRepository.Object);

            for (int i = 0; i < 1000; i++)
            {
                fakeAddresses.Add(generator.GenerateFakeAddress());
            }
        }
        [Theory]
        [InlineData(0, "QRØiØåB", "726", "h-558", "90")]
        [InlineData(5, "gGåVEE", "339D", "24", "92")]
        [InlineData(8, "åNKnHifB", "905E", "tv", "55")]
        [InlineData(52, "YÆgDWbW", "892B", "35", "st")]
        [InlineData(79, "XaIæeQVE", "997", "f596", "30")]
        public void GenerateFakeAddress_Returns_CorrectOutput(int seed, string street, string number, string door, string floor)
        {
            (string postalCode, string townName) = ("1234", "TestTown");
            var mockPostalCodeRepository = new Mock<IPostalCodeRepository>();

            mockPostalCodeRepository
                .Setup(repo => repo.GetPostalCodesAndTowns())
                .Returns(
                [
                    (postalCode, townName),
                ]);
            Random seededRandom = new(seed);
            FakeAddressGenerator generator = new(mockPostalCodeRepository.Object, seededRandom);
            FakeAddress fakeAddress = generator.GenerateFakeAddress();

            Assert.StartsWith(street, fakeAddress.Street);
            Assert.Equal(fakeAddress.Number, number);
            Assert.Equal(fakeAddress.Door, door);
            Assert.Equal(fakeAddress.Floor, floor);
            Assert.Equal(fakeAddress.PostalCode, postalCode);
            Assert.Equal(fakeAddress.TownName, townName);

        }

        [Fact]
        public void GenerateFakeAddress_ThrowsException_When_PostalCodesList_IsEmpty()
        {
            var mockPostalCodeRepository = new Mock<IPostalCodeRepository>();

            mockPostalCodeRepository
                .Setup(repo => repo.GetPostalCodesAndTowns())
                .Returns([]);
            FakeAddressGenerator generator = new(mockPostalCodeRepository.Object);

            Assert.Throws<Exception>(() => generator.GenerateFakeAddress());
        }

        [Theory]
        [InlineData(null, "Glostrup")]
        [InlineData(null, null)]
        [InlineData("2000", null)]
        public void GenerateFakeAddress_ThrowsException_When_PostalCodeOrTownName_IsNull(string postalCode, string townName)
        {
            var mockPostalCodeRepository = new Mock<IPostalCodeRepository>();

            mockPostalCodeRepository
                .Setup(repo => repo.GetPostalCodesAndTowns())
                .Returns([(postalCode, townName)]);
            FakeAddressGenerator generator = new(mockPostalCodeRepository.Object);

            Assert.Throws<Exception>(() => generator.GenerateFakeAddress());
        }

        [Fact]
        public void GeneratedNumber_AlwaysIn_ExpectedRange()
        {
            fakeAddresses.ForEach(fakeAddress =>
            {
                int number = int.Parse(Regex.Replace(fakeAddress.Number, "[^0-9]", ""));

                Assert.InRange(number, 1, 999);
            });
        }

        [Fact]
        public void GeneratedStreet_AlwaysMatches_ExpectedPattern()
        {
            string expectedPattern = "^[A-Za-zÆØÅæøå ]+$";

            fakeAddresses.ForEach(fakeAddress =>
            {
                Assert.Matches(expectedPattern, fakeAddress.Street);
            });
        }

        [Fact]
        public void GeneratedDoor_AlwaysMatches_ExpectedPattern()
        {
            // Matching one of the 3 different door patterns (separated by "|")
            string expectedPattern = @"^(th|mf|tv|\d{1,2}|[a-z]-?\d{1,3})$";

            fakeAddresses.ForEach(fakeAddress =>
            {
                Assert.Matches(expectedPattern, fakeAddress.Door);
            });
        }

        [Fact]
        public void GeneratedFloor_AlwaysMatches_ExpectedPattern()
        {
            string expectedPattern = "^(st|[1-9][0-9]?)$";

            fakeAddresses.ForEach(fakeAddress =>
            {
                Assert.Matches(expectedPattern, fakeAddress.Floor);
            });
        }
    }
}

