using System;
using System.Collections.Generic;
using System.Linq;

namespace testing_mandatory_backend.Services;

public class PhoneNumberGenerator
{
    // Use a HashSet to store valid prefixes for efficient lookups and avoid duplicate entries
    private static readonly HashSet<string> ValidPrefixes = new HashSet<string>
    {
        "2", "30", "31", "40", "41", "42", "50", "51", "52", "53",
        "60", "61", "71", "81", "91", "92", "93",
        "342", "344", "345", "346", "347", "348", "349",
        "356", "357", "359", "362", "365", "366",
        "389", "398", "431", "441", "462", "466", "468", "472",
        "474", "476", "478", "485-486", "488-489",
        "493-496", "498-499",
        "542-543", "545",
        "551-552", "556",
        "571-574",
        "782-783", "785-786", "788-789", "826-827", "829"
    };

    // Expanded valid prefixes to handle ranges like "485-486"
    private static readonly HashSet<string> ExpandedValidPrefixes = ExpandValidPrefixes();

    private static HashSet<string> ExpandValidPrefixes()
    {
        var prefixes = new HashSet<string>();

        foreach (var validPrefix in ValidPrefixes)
        {
            if (validPrefix.Contains("-"))
            {
                var parts = validPrefix.Split('-');
                if (int.TryParse(parts[0], out int start) && int.TryParse(parts[1], out int end))
                {
                    for (int i = start; i <= end; i++)
                    {
                        prefixes.Add(i.ToString());
                    }
                }
            }
            else
            {
                prefixes.Add(validPrefix);
            }
        }

        return prefixes;
    }

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
        if (remainingLength <= 0)
        {
            throw new ArgumentException("Prefix is too long for the phone number length");
        }

        // Generate the random digits required to complete the phone number
        string randomDigits = GenerateRandomDigits(remainingLength);

        // Concatenate the prefix and the random digits to form the full phone number
        return prefix + randomDigits;
    }

    // Method to generate multiple phone numbers in bulk
    public List<string> GenerateBulkPhoneNumbers(int count)
    {
        var phoneNumbers = new HashSet<string>();
        var prefixes = ExpandedValidPrefixes.ToList();

        // Shuffle the list for better randomness
        Shuffle(prefixes);

        int prefixIndex = 0;

        while (phoneNumbers.Count < count)
        {
            if (prefixIndex >= prefixes.Count)
            {
                // If we have exhausted all prefixes, reshuffle and start over
                Shuffle(prefixes);
                prefixIndex = 0;
            }

            var prefix = prefixes[prefixIndex];
            var phoneNumber = GeneratePhoneNumber(prefix);

            // Ensure uniqueness
            if (phoneNumbers.Add(phoneNumber))
            {
                prefixIndex++;
            }
        }

        return phoneNumbers.Take(count).ToList();
    }

    // Method to validate if the prefix is valid
    private bool IsValidPrefix(string prefix)
    {
        return ExpandedValidPrefixes.Contains(prefix);
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

    // Method to shuffle a list (Fisher-Yates shuffle algorithm)
    private void Shuffle<T>(IList<T> list)
    {
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = RandomGenerator.Next(n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }
}