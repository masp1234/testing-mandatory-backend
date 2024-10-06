using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend
{
    public class FakeAddressGenerator
    {
        private readonly Random random;
        private readonly IPostalCodeRepository postalCodeRepository;
        private readonly char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };

        public FakeAddressGenerator(IPostalCodeRepository postalCodeRepository, Random random = null)
        {
            this.postalCodeRepository = postalCodeRepository;
            this.random = random ?? new Random();
        }

        public FakeAddress GenerateFakeAddress()
        {
            string number = GenerateNumber();
            string door = GenerateDoor();
            string floor = GenerateFloor();
            (string postalCode, string townName) = GeneratePostalCodeAndTownName();

            return new FakeAddress(number, door, floor, postalCode, townName);
        }

        private string GenerateNumber()
        {
            string numberString = random.Next(1, 1000).ToString();
            int optionalLetter = random.Next(0, 2);
            if (optionalLetter == 0)
            {
                numberString += GetRandomLetter();
            }
            return numberString;
        }

        private string GenerateDoor()
        {
            int randomFormat = random.Next(0, 3);

            string doorString = "";

            switch (randomFormat)
            {
                case 0:
                    string[] doorFormats = { "th", "mf", "tv" };
                    doorString += doorFormats[random.Next(0, doorFormats.Length)];
                    break;
                case 1:
                    int numberBetween0And50 = random.Next(1, 51);
                    doorString += numberBetween0And50.ToString();
                    break;
                case 2:
                    doorString += GetRandomLetter().ToString().ToLower();
                    int optionalDash = random.Next(0, 2);
                    if (optionalDash == 0)
                    {
                        doorString += "-";
                    }

                    doorString += random.Next(1, 999).ToString();
                    break;

            }
            return doorString;
        }

        private string GenerateFloor()
        {
            string floor = "";
            int randomFloor = random.Next(0, 100);
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
            int randomLetterIndex = random.Next(0, this.letters.Length);
            return this.letters[randomLetterIndex];
        }

        private (string postalCode, string townName) GeneratePostalCodeAndTownName()
        {
            var postalCodes = postalCodeRepository.GetPostalCodesAndTowns();
            int randomIndex = random.Next(postalCodes.Count);
            return postalCodes[randomIndex];
        }
    }
}
