using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Services
{
    public class PersonDataService
    {
        private IBirthdayGenerator _birthdayGenerator;
        private IPhoneNumberGenerator _phoneNumberGenerator;
        private INameAndGenderGenerator _nameAndGenderGenerator;
        private IFakeAddressGenerator _fakeAddressGenerator;
        public PersonDataService(IBirthdayGenerator birthdayGenerator,
                                 IPhoneNumberGenerator phoneNumberGenerator,
                                 INameAndGenderGenerator nameAndGenderGenerator,
                                 IFakeAddressGenerator fakeAddressGenerator)
        {
            _birthdayGenerator = birthdayGenerator;
            _phoneNumberGenerator = phoneNumberGenerator;
            _nameAndGenderGenerator = nameAndGenderGenerator;
            _fakeAddressGenerator = fakeAddressGenerator;
        }

        public List<PersonData> GenerateBulkPersonData(int amount)
        {
            if (amount < 2 || amount > 100)
            {
                throw new ArgumentException("The amount has to be between 2 and 100 inclusive");
            }
            List<PersonData> peopleData = [];
            for (int i = 0; i < amount; i++)
            {
                peopleData.Add(CreatePersonData());
                    
            }
            return peopleData;


        }

        public PersonData CreatePersonData()
        {
            PersonData personData;
            try
            {
                DateTime birthday = _birthdayGenerator.GenerateRandomBirthday();
                string phoneNumber = _phoneNumberGenerator.GeneratePhoneNumber(null);
                FakeAddress address = _fakeAddressGenerator.GenerateFakeAddress();
                Person person = _nameAndGenderGenerator.GenerateNameAndGender();
                // string cpr = _cprGenerator.GenerateCPR(birthday, person.Gender);
               personData = new(
                    address,
                    person,
                    birthday,
                    phoneNumber,
                    "temp cpr"
                    );

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw new InvalidOperationException("Failed to generate person data.", ex);
            }
            return personData;
        }
    }
}
