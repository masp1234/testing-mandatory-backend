using testing_mandatory_backend.Models;
using System.Text.Json;

namespace testing_mandatory_backend.Services
{
    public class PersonService
    {
        
        public List<Person> GetPersonFromJsonFile(string gender, int count)
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "./Data", "person-names.json");
            var jsonData = File.ReadAllText(jsonFilePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var personsData = JsonSerializer.Deserialize<List<Person>>(jsonData, options);
            
    


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
             // Generate and return `count` random persons
            Random random = new();
            List<Person> randomPersons = new();

            for (int i = 0; i < count; i++)
            {
                int randomIndex = random.Next(filteredPersons.Count);
                randomPersons.Add(filteredPersons[randomIndex]);
            }

            return randomPersons;
            
        }

        public void PrintPerson(List<Person> persons)
        {
            foreach (var person in persons)
            {
                Console.WriteLine($"Name: {person.Name}, Surname: {person.Surname}, Gender: {person.Gender}");
            }
        }
    }
}