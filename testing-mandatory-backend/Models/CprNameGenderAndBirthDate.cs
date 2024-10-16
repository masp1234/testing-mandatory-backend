using Org.BouncyCastle.Asn1.Mozilla;

namespace testing_mandatory_backend.Models
{
    public class CprNameGenderAndBirthDate
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }

        public string CPR { get; set; }
        public CprNameGenderAndBirthDate(string name, string surname, string gender, DateTime birthDate, string cpr)
        {
            CPR = cpr;
            Name = name;
            Surname = surname;
            Gender = gender;
            BirthDate = birthDate;

        }
    }
}