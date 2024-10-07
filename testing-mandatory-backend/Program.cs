
using testing_mandatory_backend.Service;

public class Program
{
    public static void Main()
    {
        PhoneNumberGenerator generator = new PhoneNumberGenerator();
        try
        {
            // Generate a phone number with a specific prefix
            Console.WriteLine("Generated Phone Number: " + generator.GeneratePhoneNumber("342"));
        }
        catch (ArgumentException ex)
        {
            // Handle invalid prefix error
            Console.WriteLine("Error: " + ex.Message);
        }

        // Generate multiple phone numbers in bulk
        List<string> bulkNumbers = generator.GenerateBulkPhoneNumbers(10);
        Console.WriteLine("Bulk Generated Numbers: ");
        bulkNumbers.ForEach(Console.WriteLine); // Print each generated phone number
    }
}
