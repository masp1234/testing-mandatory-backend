using testing_mandatory_backend.Models;
using System.Text.Json;

namespace testing_mandatory_backend.Repositories{
    public class NameAndGenderRepository: INameAndGenderRepository
    {
        public List<NameAndGender> GetNameAndGender()
        {
            var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "./Data", "person-names.json");
            var jsonData = File.ReadAllText(jsonFilePath);
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };

            var personsData = JsonSerializer.Deserialize<List<NameAndGender>>(jsonData, options);
            if (personsData == null || personsData.Count == 0)
            {
                throw new Exception("No persons found in the JSON data.");
            }

            return personsData;
        }
    }
}