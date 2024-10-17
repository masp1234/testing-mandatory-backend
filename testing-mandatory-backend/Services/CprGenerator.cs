namespace testing_mandatory_backend.Services {
    
    public class CprGenerator : ICprGenerator {
        
        private static readonly Random random = new Random();

        public string GenerateRandomCpr(DateTime birthday) {

            // Formats the date as ddMMyy
            string datePart = birthday.ToString("ddMMyy");

            // Generate the last four random digits
            string randomDigits = random.Next(1000, 10000).ToString("D4");

            // Combine the date part and the random digits to form the CPR code
            return datePart + randomDigits;
        }

        public string GenerateCprWithBirthdayAndGender(DateTime birthday, string gender) {

            // Format the date-part of the CPR
            string birthdayPart = birthday.ToString("ddMMyy");
            
            Random random = new Random();
            int sequenceNumber = random.Next(100, 1000); // Generate a random sequence number in this range, ensuring 3 digits

            // Ensures the last digit is odd for males and even for females
            int lastDigit = random.Next(0, 10);
            if (gender.Equals("male", StringComparison.OrdinalIgnoreCase)) {
                if (lastDigit % 2 == 0) {
                    lastDigit++;
                }
            }
            else if (gender.Equals("female", StringComparison.OrdinalIgnoreCase)) {
                if (lastDigit % 2 != 0) {
                    lastDigit--;
                }
            }

            // Combine the parts to form the CPR number
            string cpr = $"{birthdayPart}{sequenceNumber:D3}{lastDigit}";

            return cpr;
        }

    }
}