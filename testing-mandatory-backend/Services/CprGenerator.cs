namespace testing_mandatory_backend.Services
{
    public class CprGenerator
    {
        private static readonly Random random = new Random();

        public string GenerateRandomCpr()
        {
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

        public string GenerateCprWithBirthdayAndGender() {

            return "test";
        }
    }
}