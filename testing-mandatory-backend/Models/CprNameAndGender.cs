using Org.BouncyCastle.Asn1.Mozilla;

namespace testing_mandatory_backend.Models
{
    public class CprNameAndGender
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }

        public string CPR { get; set; }
        public CprNameAndGender(string name, string surname, string gender, string cpr)
        {
            CPR = cpr;
            Name = name;
            Surname = surname;
            Gender = gender;

        }
    }
}