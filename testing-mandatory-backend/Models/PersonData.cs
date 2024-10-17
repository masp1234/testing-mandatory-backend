namespace testing_mandatory_backend.Models
{
    public class PersonData
    {
        public FakeAddress FakeAddress { get; set; }

        public NameAndGender Person { get; set; }

        public DateTime Birthday { get; set; }

        public string PhoneNumber { get; set; }

        public string CPR { get; set; }

        public PersonData(
            FakeAddress fakeAddress,
            NameAndGender person,
            DateTime birthday,
            string phoneNumber,
            string cpr
            )
        {
            FakeAddress = fakeAddress;
            Person = person;
            Birthday = birthday;
            PhoneNumber = phoneNumber;
            CPR = cpr;

        }
    }
}
