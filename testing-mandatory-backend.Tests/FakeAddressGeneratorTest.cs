using Moq;
using testing_mandatory_backend;
using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend.Tests
{
    public class FakeAddressGeneratorTest
    {
        [Theory]
        [InlineData(0, "726", "h-558", "90")]
        [InlineData(5, "339D", "24", "92")]
        [InlineData(8, "905E", "tv", "55")]
        [InlineData(52, "892B", "35", "st")]
        [InlineData(79, "997", "f596", "30")]
        public void FakeAddress_Returns_Correct_Output(int seed, string number, string door, string floor)
        {
            var mockPostalCodeRepository = new Mock<IPostalCodeRepository>();

            mockPostalCodeRepository
                .Setup(repo => repo.GetPostalCodesAndTowns())
                .Returns(
                [
                    ("1234", "TestTown"),
                ]);
            Random seededRandom = new (seed);
            FakeAddressGenerator generator = new (mockPostalCodeRepository.Object, seededRandom);
            FakeAddress fakeAddress = generator.GenerateFakeAddress();

            Assert.Equal(fakeAddress.Number, number);
            Assert.Equal(fakeAddress.Door, door);
            Assert.Equal(fakeAddress.Floor, floor);

        }
    }
}
