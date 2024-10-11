using testing_mandatory_backend.Models;
using System.Text.Json;
using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend.Services
{
    public class NameAndGenderGenerator
    {
        private readonly INameAndGenderRepository _NameAndGenderRepository;


        public NameAndGenderGenerator(INameAndGenderRepository nameAndGenderRepository)
        {
            _NameAndGenderRepository = nameAndGenderRepository;
        }
        
        public Person GetNameAndGenderFromJsonFile(string gender)
        {
            var personsData = _NameAndGenderRepository.GetNameAndGender();
            
            if (personsData == null || personsData.Count == 0)
            {
                throw new Exception("No persons found in the JSON data.");
            }

            var filteredPersons = personsData
                .Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (filteredPersons.Count == 0)
            {
                throw new Exception($"No persons found with gender '{gender}'.");
            }

            Random random = new();
            int randomIndex = random.Next(filteredPersons.Count);
            return filteredPersons[randomIndex];
            
        }

        public void PrintPerson(Person person)
        {
            
            Console.WriteLine($"Name: {person.Name}, Surname: {person.Surname}, Gender: {person.Gender}");
            
        }
    }
}