namespace testing_mandatory_backend.Models
{
    public class NameAndGender
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public NameAndGender(string name, string surname, string gender)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            
        }
    }
}