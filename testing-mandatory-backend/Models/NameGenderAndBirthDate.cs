using Org.BouncyCastle.Asn1.Mozilla;

namespace testing_mandatory_backend.Models
{
    public class NameGenderAndBirthDate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public NameGenderAndBirthDate(string name, string surname, string gender, DateTime birthDate)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            BirthDate = birthDate;

        }
    }
}
