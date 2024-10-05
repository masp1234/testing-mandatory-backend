using MySql.Data.MySqlClient;
using System;
using System.CodeDom.Compiler;
using testing_mandatory_backend.Database;

namespace testing_mandatory_backend
{
    public class FakeAddress
    {
        public FakeAddress(Random random = null)
        {
            this.random = random ?? new Random();
            this.Number = GenerateNumber();
            this.Door = GenerateDoor();
            this.Floor = GenerateFloor();
            (string postalCode, string townName) = GeneratePostalCodeAndTownName();
            this.PostalCode = postalCode;
            this.TownName = townName;
        }
        private readonly char[] letters = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M' };

        private readonly Random random;
        public string Number { get; }
        public string Door { get; }

        public string Floor { get; }

        public string PostalCode { get; }

        public String TownName { get; }

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
            MySqlConnection connection = DbConnection.GetConnection();
            MySqlCommand command = new("SELECT * FROM postal_code", connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<(string postalCode, string townName)> postalCodes = [];

            while (reader.Read())
            {
                postalCodes.Add((reader.GetString(0), reader.GetString(1)));

            }
            reader.Close();

            int randomPostalCodeAndTownIndex = random.Next(0, postalCodes.Count);
            return postalCodes[randomPostalCodeAndTownIndex];
        }


    }
}
