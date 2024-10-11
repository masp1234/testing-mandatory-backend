using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Repositories
{
    public interface INameAndGenderRepository
    {
        List<Person> GetNameAndGender();
    }
}