namespace testing_mandatory_backend.Services
{
    public interface ICprGenerator
    {
        public string GenerateCprFromBirthdayAndGender(DateTime birthday, string gender);

        public string GenerateRandomCpr();
    }
}
