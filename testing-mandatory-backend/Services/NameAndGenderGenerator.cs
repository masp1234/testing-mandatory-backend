using testing_mandatory_backend.Models;
using System.Text.Json;
using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend.Services
{
    public class NameAndGenderGenerator: INameAndGenderGenerator
    {
        private readonly INameAndGenderRepository _NameAndGenderRepository;


        public NameAndGenderGenerator(INameAndGenderRepository nameAndGenderRepository)
        {
            _NameAndGenderRepository = nameAndGenderRepository;
        }
        
        public NameAndGender GenerateNameAndGender()
        {
            var people = _NameAndGenderRepository.GetNameAndGender();
            
            if (people == null || people.Count == 0)
            {
                throw new Exception("No persons found in the JSON data.");
            }

            Random random = new();
            int randomIndex = random.Next(people.Count);
            return people[randomIndex];
            
        }
    }
}