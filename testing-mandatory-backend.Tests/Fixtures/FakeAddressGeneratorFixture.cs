using Moq;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend;

public class FakeAddressGeneratorFixture
{
    public List<FakeAddress> FakeAddresses { get; }
    public List<(string postalCode, string townName)> PostalCodesAndTowns { get; }

    public FakeAddressGeneratorFixture()
    {
        PostalCodesAndTowns = [
                    ("1234", "TestTown"),
                    ("3456", "AnotherTown"),
                    ("3456", "AnotherTown"),
                    ("3456", "AnotherTown"),
                    ];

        Mock<IPostalCodeRepository> mockRepository = new Mock<IPostalCodeRepository>();
        mockRepository.Setup(repo => repo.GetPostalCodesAndTowns())
            .Returns(PostalCodesAndTowns);

        FakeAddressGenerator generator = new(mockRepository.Object);

        FakeAddresses = new List<FakeAddress>();
        for (int i = 0; i < 1000; i++)
        {
            FakeAddresses.Add(generator.GenerateFakeAddress());
        }
    }
}
