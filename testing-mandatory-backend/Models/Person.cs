namespace testing_mandatory_backend.Models
{
    public class Person
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public Person(string name, string surname, string gender)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            
        }
    }
    public class PersonsData
    {
        public List<Person> Persons { get; set; }

        public PersonsData(List<Person> persons){
            Persons = persons;
        }

  
    }



}