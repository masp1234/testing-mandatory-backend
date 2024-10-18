namespace testing_mandatory_backend.Models
{
    public class PersonData
    {
        public FakeAddress FakeAddress { get; set; }

        public NameAndGender Person { get; set; }

        public DateTime BirthDate { get; set; }

        public string PhoneNumber { get; set; }

        public string CPR { get; set; }

        public PersonData(
            FakeAddress fakeAddress,
            NameAndGender person,
            DateTime birthDate,
            string phoneNumber,
            string cpr
            )
        {
            FakeAddress = fakeAddress;
            Person = person;
            BirthDate = birthDate;
            PhoneNumber = phoneNumber;
            CPR = cpr;

        }
    }
}
