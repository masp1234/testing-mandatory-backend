using System;
using System.Collections.Generic;
using System.Linq;

namespace testing_mandatory_backend.Service;




public class PhoneNumberGenerator
{
    // Use a HashSet to store valid prefixes for efficient lookups and avoid duplicate entries
    private static readonly HashSet<string> ValidPrefixes = new HashSet<string>
    {
        "2", "30", "31", "40", "41", "42", "50", "51", "52", "53", "60", "61", "71", "81", "91", "92", "93",
        "342", "344", "345", "346", "347", "348", "349", "356", "357", "359", "362", "365", "366", "389", "398",
        "431", "441", "462", "466", "468", "472", "474", "476", "478", "485", "486", "488", "489", "493", "494", "495", "496",
        "498", "499", "542", "543", "545", "551", "552", "556", "571", "572", "573", "574", "577", "579", "584", "586", "587",
        "589", "597", "598", "627", "629", "641", "649", "658", "662", "663", "664", "665", "667", "692", "693", "694", "697",
        "771", "772", "782", "783", "785", "786", "788", "789", "826", "827", "829"
    };

    // Random number generator used for generating random digits in phone numbers
    private static readonly Random RandomGenerator = new Random();
    private const int PhoneNumberLength = 8; // Standard phone number length required

    // Method to generate a phone number based on a given prefix
    public string GeneratePhoneNumber(string prefix)
    {
        // Validate if the prefix is part of the valid set of prefixes
        if (!IsValidPrefix(prefix))
        {
            throw new ArgumentException("Invalid prefix for phone number generation");
        }

        // Calculate how many more digits are needed to complete the phone number length
        int remainingLength = PhoneNumberLength - prefix.Length;
        // Generate the random digits required to complete the phone number
        string randomDigits = GenerateRandomDigits(remainingLength);

        // Concatenate the prefix and the random digits to form the full phone number
        return prefix + randomDigits;
    }

    // Method to generate multiple phone numbers in bulk
    public List<string> GenerateBulkPhoneNumbers(int count)
    {
        HashSet<string> phoneNumbers = new HashSet<string>(); // Use HashSet to ensure uniqueness of phone numbers

        // Keep generating phone numbers until the required count is reached
        while (phoneNumbers.Count < count)
        {
            string prefix = GetRandomValidPrefix(); // Get a random valid prefix
            string phoneNumber = GeneratePhoneNumber(prefix); // Generate phone number with the selected prefix
            phoneNumbers.Add(phoneNumber); // Add to HashSet to avoid duplicates
        }

        return phoneNumbers.ToList(); // Convert HashSet to List for return
    }

    // Method to validate if the prefix is valid
    private bool IsValidPrefix(string prefix)
    {
        // Check if the prefix is in the set of valid prefixes and that it only contains digits
        return ValidPrefixes.Contains(prefix) && prefix.All(char.IsDigit);
    }

    // Method to generate a string of random numeric digits of a given length
    private string GenerateRandomDigits(int length)
    {
        char[] digits = new char[length];
        for (int i = 0; i < length; i++)
        {
            // Generate a random digit between '0' and '9'
            digits[i] = (char)('0' + RandomGenerator.Next(10));
        }
        return new string(digits);
    }

    // Method to get a random prefix from the set of valid prefixes
    private string GetRandomValidPrefix()
    {
        // Generate a random index to pick a prefix from the ValidPrefixes HashSet
        int index = RandomGenerator.Next(ValidPrefixes.Count);
        return ValidPrefixes.ElementAt(index); // Convert HashSet to an indexed collection for selection
    }
}

