using testing_mandatory_backend.Models;
using System.Text.Json;

namespace testing_mandatory_backend.Services
{
    public class PersonService
    {
        
        public Person GetPersonFromJsonFile(string gender)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "./Data", "person-names.json");
            var jsonData = File.ReadAllText(jsonFilePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var personsData = JsonSerializer.Deserialize<List<Person>>(jsonData, options);
            
    

            //Console.WriteLine("her is personData " + personsData!);

            if (personsData == null || personsData.Count == 0)
            {
                throw new Exception("No persons found in the JSON data.");
            }

            

            // Filter the persons based on the specified gender
            var filteredPersons = personsData
                .Where(p => p.Gender.Equals(gender, StringComparison.OrdinalIgnoreCase))
                .ToList();

            // Check if any persons match the specified gender
            if (filteredPersons.Count == 0)
            {
                throw new Exception($"No persons found with gender '{gender}'.");
            }
            // Get a random person
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