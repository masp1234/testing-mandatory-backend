using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Services
{
    public interface IPersonDataService
    {
        public List<PersonData> GenerateBulkPersonData(int amount);
        public PersonData CreatePersonData();

        public string CreatePhoneNumber();

        public NameAndGender CreateNameAndGender();

        public NameGenderAndBirthDate CreateNameAndGenderAndBirthDate();

        public FakeAddress CreateAddress();

        public string CreateCPR();
        public CprNameAndGender CreateCprNameAndGender();

        public CprNameGenderAndBirthDate CreateCprNameGenderAndBirthDate();
    }
}
