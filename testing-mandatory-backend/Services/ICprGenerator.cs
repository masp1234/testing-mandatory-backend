namespace testing_mandatory_backend.Services
{
    public interface ICprGenerator {
        public string GenerateCprWithBirthdayAndGender(DateTime birthday, string gender);

        public string GenerateRandomCpr(DateTime birthday);
    }
}
