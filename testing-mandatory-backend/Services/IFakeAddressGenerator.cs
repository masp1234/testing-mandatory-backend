using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Services
{
    public interface IFakeAddressGenerator
    {
        public FakeAddress GenerateFakeAddress();
    }
}
