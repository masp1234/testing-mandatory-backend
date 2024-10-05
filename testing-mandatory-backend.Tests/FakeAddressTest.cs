using testing_mandatory_backend;

namespace testing_mandatory_backend.Tests
{
    public class FakeAddressTest
    {
        [Theory]
        [InlineData(0, "726", "h-558", "90", "5800", "Nyborg")]
        [InlineData(5, "339D", "24", "92", "3770", "Allinge")]
        [InlineData(8, "905E", "tv", "55", "6094", "Hejls")]
        [InlineData(52, "892B", "35", "st", "4330", "Hvalsø")]
        [InlineData(79, "997", "f596", "30", "2980", "Kokkedal")]
        public void FakeAddress_Returns_Correct_Output(int seed, string number, string door, string floor, string postalCode, string townName)
        {
            Random seededRandom = new (seed);
            FakeAddress fakeAddress = new (seededRandom);

            Assert.Equal(fakeAddress.Number, number);
            Assert.Equal(fakeAddress.Door, door);
            Assert.Equal(fakeAddress.Floor, floor);
            Assert.Equal(fakeAddress.PostalCode, postalCode);
            Assert.Equal(fakeAddress.TownName, townName);

        }
    }
}
