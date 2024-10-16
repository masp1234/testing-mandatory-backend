using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Models;

namespace testing_mandatory_backend.Services {
    public class CprGenerator {
        private static readonly Random random = new Random();

        public string GenerateRandomCpr() {
            // Generates a random date that's between 18 and 100 years ago
            DateTime startDate = DateTime.Today.AddYears(-100);
            DateTime endDate = DateTime.Today.AddYears(-18);
            int range = (endDate - startDate).Days;
            DateTime randomDate = startDate.AddDays(random.Next(range));

            // Formats the date as ddMMyy
            string datePart = randomDate.ToString("ddMMyy");

            // Generate the last four random digits
            string randomDigits = random.Next(1000, 10000).ToString("D4");

            // Combine the date part and the random digits to form the CPR code
            return datePart + randomDigits;
        }

        public (string cpr, Person randomPerson, DateTime randomBirthday) GenerateCprWithBirthdayAndGender(string gender) {
            // Generate a random birthday
            DateTime randomBirthday = BirthdayGenerator.GenerateRandomBirthday();

            // Create a new instance of the NameAndGenderGenerator class
            NameAndGenderGenerator nameAndGenderGenerator = new NameAndGenderGenerator(new NameAndGenderRepository());

            // Get a random person with the specified gender
            Person randomPerson = nameAndGenderGenerator.GetNameAndGenderFromJsonFile(gender);

            // Format the date-part of the CPR
            string birthdayPart = randomBirthday.ToString("ddMMyy");

            Random random = new Random();
            int sequenceNumber = random.Next(100, 1000); // Generate a random sequence number in this range

            // Ensures the last digit is odd for males and even for females
            int lastDigit = random.Next(0, 10);
            if (gender.Equals("male", StringComparison.OrdinalIgnoreCase)) {
                if (lastDigit % 2 == 0)
                {
                    lastDigit++;
                }
            }
            else if (gender.Equals("female", StringComparison.OrdinalIgnoreCase)) {
                if (lastDigit % 2 != 0)
                {
                    lastDigit--;
                }
            }

            // Combine the parts to form the CPR number
            string cpr = $"{birthdayPart}-{sequenceNumber:D3}{lastDigit}";

            return (cpr, randomPerson, randomBirthday);
        }

        public void PrintCprWithBirthdayAndGender(string gender)
{
            // Call the GenerateCprWithBirthdayAndGender method
            var result = GenerateCprWithBirthdayAndGender(gender);

            // Extract the values from the result
            string cpr = result.cpr;
            Person randomPerson = result.randomPerson;
            DateTime randomBirthday = result.randomBirthday;

            // Print the results to the console
            Console.WriteLine($"CPR: {cpr}");
            Console.WriteLine($"Person: {randomPerson.Name}, Gender: {randomPerson.Gender}");
            Console.WriteLine($"Birthday: {randomBirthday.ToShortDateString()}");
        }
    }
}