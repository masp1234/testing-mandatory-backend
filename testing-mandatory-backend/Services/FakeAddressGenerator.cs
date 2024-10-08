using testing_mandatory_backend.Models;
using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend.Services
{
    public class FakeAddressGenerator
    {
        private readonly Random _random;
        private readonly IPostalCodeRepository _postalCodeRepository;
        private readonly char[] _letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };

        public FakeAddressGenerator(IPostalCodeRepository postalCodeRepository, Random? random = null)
        {
            _postalCodeRepository = postalCodeRepository;
            _random = random ?? new Random();
        }

        public FakeAddress GenerateFakeAddress()
        {

            string number = GenerateNumber();
            string door = GenerateDoor();
            string floor = GenerateFloor();
            (string postalCode, string townName) = GeneratePostalCodeAndTownName();
            string street = GenerateStreet();

            return new FakeAddress(street, number, door, floor, postalCode, townName);
        }
        private string GenerateStreet()
        {
            string street = "";
            string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅabcdefghijklmnopqrstuvwxyzæøå ";
            int numberOfCharacters = _random.Next(2, 101);

            for (int i = 0; i < numberOfCharacters; i++)
            {
                bool characterIsASpace = true;
                while (characterIsASpace)
                {
                    char tempChar = allowedCharacters[_random.Next(allowedCharacters.Length)];
                    if (tempChar.ToString() != " ")
                    {
                        street += tempChar;
                        characterIsASpace = false;
                    }
                }
            }

            return street;
        }

        private string GenerateNumber()
        {
            string numberString = _random.Next(1, 1000).ToString();
            int optionalLetter = _random.Next(0, 2);
            if (optionalLetter == 0)
            {
                numberString += GetRandomLetter();
            }
            return numberString;
        }

        private string GenerateDoor()
        {
            int randomFormat = _random.Next(0, 3);

            string doorString = "";

            switch (randomFormat)
            {
                case 0:
                    string[] doorFormats = { "th", "mf", "tv" };
                    doorString += doorFormats[_random.Next(0, doorFormats.Length)];
                    break;
                case 1:
                    int numberBetween0And50 = _random.Next(1, 51);
                    doorString += numberBetween0And50.ToString();
                    break;
                case 2:
                    doorString += GetRandomLetter().ToString().ToLower();
                    int optionalDash = _random.Next(0, 2);
                    if (optionalDash == 0)
                    {
                        doorString += "-";
                    }

                    doorString += _random.Next(1, 999).ToString();
                    break;

            }
            return doorString;
        }

        private string GenerateFloor()
        {
            string floor = "";
            int randomFloor = _random.Next(0, 100);
            if (randomFloor == 0)
            {
                floor = "st";
            }
            else
            {
                floor = randomFloor.ToString();

            }
            return floor;
        }

        private char GetRandomLetter()
        {
            int randomLetterIndex = _random.Next(0, _letters.Length);
            return _letters[randomLetterIndex];
        }

        private (string postalCode, string townName) GeneratePostalCodeAndTownName()
        {
            var postalCodes = _postalCodeRepository.GetPostalCodesAndTowns();
            if (postalCodes.Count == 0)
            {
                throw new Exception("No postalcodes were found.");
            }
            int randomIndex = _random.Next(postalCodes.Count);
            (string postalCode, string townName) = postalCodes[randomIndex];
            if (string.IsNullOrEmpty(postalCode) || string.IsNullOrEmpty(townName))
            {
                throw new Exception("Postal code or town name missing.");
            }
            return (postalCode, townName);
        }
    }
}
