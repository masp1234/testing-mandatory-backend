using testing_mandatory_backend.Repositories;
using Xunit;

namespace testing_mandatory_backendTests.Integration
{
    [Trait("Category", "Integration")]
    [Collection("Sequential")]
    public class PostalCodeRepositoryIntegrationTest: IClassFixture<TestDatabaseFixture>
    {
        private readonly TestDatabaseFixture _fixture;
        private readonly PostalCodeRepository _repository;
        public PostalCodeRepositoryIntegrationTest(TestDatabaseFixture fixture)
        {
            
            _fixture = fixture;
            _fixture.CreateNewConnection();
            _repository = new(_fixture.Connection);
            _fixture.ResetDatabase();
        }

        [Fact]
        public void GetPostalCodesAndTowns_ShouldReturn_CorrectAmount()
        {
            _fixture.SeedDatabase(("1234", "Town"), ("9876", "AnotherTown"));
            List<(string, string)> postalCodesAndTownNames = _repository.GetPostalCodesAndTowns();
            Assert.True(postalCodesAndTownNames.Count == 2);
        }

        [Fact]
        public void GetPostalCodesAndTowns_ShouldReturnEmpty_WhenTableEmpty()
        {
            List<(string, string)> postalCodesAndTownNames = _repository.GetPostalCodesAndTowns();
            Assert.Empty(postalCodesAndTownNames);
        }

        [Fact]
        public void GetPostalCodesAndTowns_ShouldReturnEmpty_WhenConnectionFails()
        {
            _fixture.SeedDatabase(("1234", "Town"), ("9876", "AnotherTown"));
            _fixture.Connection.Close();
            List<(string, string)> postalCodesAndTownNames = _repository.GetPostalCodesAndTowns();
            Assert.Empty(postalCodesAndTownNames);

        }
    }
}
