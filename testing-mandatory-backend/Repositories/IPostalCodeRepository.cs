namespace testing_mandatory_backend.Repositories
{
    public interface IPostalCodeRepository
    {
        List<(string postalCode, string townName)> GetPostalCodesAndTowns();
    }
}
