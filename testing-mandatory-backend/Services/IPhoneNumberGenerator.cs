namespace testing_mandatory_backend.Services
{
    public interface IPhoneNumberGenerator
    {
        public string GeneratePhoneNumber(string prefix);

        public List<string> GenerateBulkPhoneNumbers(int count);
    }
}
