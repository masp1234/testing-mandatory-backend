using testing_mandatory_backend.Services;
using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Tests.Fixtures {
    public class CprGeneratorFixture {
        public List<string> RandomCprs { get; private set; }
        public List<CprData> RandomCprsWithGenderAndBirthday { get; private set; }
        private const int numberOfTests = 1000; // Example value, adjust as needed

        public CprGeneratorFixture() {
            RandomCprs = new List<string>();
            RandomCprsWithGenderAndBirthday = new List<CprData>();
            CprGenerator CprGenerator = new();

            for (int i = 0; i < numberOfTests; i++) {
                RandomCprs.Add(CprGenerator.GenerateRandomCpr());
            }

            Random random = new();
            for (int i = 0; i < numberOfTests; i++) {
                // Generate a random gender
                string gender = random.Next(0, 2) == 0 ? "Male" : "Female";

                // Generate a random birthday
                // Could be simplified by just using BirthdayGenerator, but that would introduce higher coupling.
                DateTime startDate = DateTime.Today.AddYears(-100);
                DateTime endDate = DateTime.Today.AddYears(-18);
                int range = (endDate - startDate).Days;
                DateTime randomBirthday = startDate.AddDays(random.Next(range));

                // Generate a random CPR with birthday and gender
                var result = CprGenerator.GenerateCprWithBirthdayAndGender(randomBirthday, gender);

                // Add the generated CPR to the list
                RandomCprsWithGenderAndBirthday.Add(new CprData {
                    Cpr = result,
                    Gender = gender,
                    Birthday = randomBirthday
                });
            }
        }
    }
}