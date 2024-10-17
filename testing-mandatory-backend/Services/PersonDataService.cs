using Org.BouncyCastle.Asn1.Esf;
using System.Reflection.Metadata.Ecma335;
using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Services
{
    public class PersonDataService
    {
        private readonly IBirthdayGenerator _birthdayGenerator;
        private readonly IPhoneNumberGenerator _phoneNumberGenerator;
        private readonly INameAndGenderGenerator _nameAndGenderGenerator;
        private readonly IFakeAddressGenerator _fakeAddressGenerator;
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
                NameAndGender person = _nameAndGenderGenerator.GenerateNameAndGender();
                // Stubbing the CPR generator, use real generator when finished
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

        public string CreatePhoneNumber()
        {
            string phoneNumber;
            try
            {
                phoneNumber = _phoneNumberGenerator.GeneratePhoneNumber(null);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("Could not create a phone number.", exception);
            }
            return phoneNumber;
        }

        public NameAndGender CreateNameAndGender()
        {
            NameAndGender nameAndGender;
            try
            {
                nameAndGender = _nameAndGenderGenerator.GenerateNameAndGender();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("Could not create name and gender.", exception);
            }

            return nameAndGender;
        }

        public NameGenderAndBirthDate CreateNameAndGenderAndBirthDate()
        {
            NameAndGender nameAndGender;
            DateTime birthDate;
            try
            {
                nameAndGender = _nameAndGenderGenerator.GenerateNameAndGender();
                birthDate = _birthdayGenerator.GenerateRandomBirthday();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("Could not create name, gender and birthdate.", exception);
            }

            NameGenderAndBirthDate nameGenderAndBirthdate = new(
                nameAndGender.Name,
                nameAndGender.Surname,
                nameAndGender.Gender,
                birthDate
                );

            return nameGenderAndBirthdate;

        }

        public FakeAddress CreateAddress()
        {
            FakeAddress fakeAddress;
            try
            {
                fakeAddress = _fakeAddressGenerator.GenerateFakeAddress();
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
                throw new Exception("Could not create a fake address.");
            }

            return fakeAddress;

        }

        /*
       public string CreateCPR()
       {
           string cpr;
           try
           {
               cpr = _cprGenerator.GenerateCPR();
           }
           catch(Exception exception)
           {
               Console.WriteLine(exception);
               throw new Exception("Could not create a CPR", exception);
           }

           // Use CPR generator when available
           return cpr;
       }
      
        public CprNameAndGender CreateCprNameAndGender()
        {
            // use gender to create a CPR

        }

        public CprNameGenderAndBirthDate CreateCprNameGenderAndBirthDate()
        {
            // use gender and birthdate to create the CPR - making sure that
            // the 4 last digits are valid in regards to the gender and that the 6 is using the birthdate
        }
        */

    }
}
