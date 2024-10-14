using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Services
{
    public interface INameAndGenderGenerator
    {
        public Person GenerateNameAndGender();
    }
}
