using System;

namespace testing_mandatory_backend
{
    public class FakeAddress
    {
        public string Number { get; }
        public string Door { get; }
        public string Floor { get; }
        public string PostalCode { get; }
        public string TownName { get; }

        public FakeAddress(string number, string door, string floor, string postalCode, string townName)
        {
            Number = number;
            Door = door;
            Floor = floor;
            PostalCode = postalCode;
            TownName = townName;
        }
    }
}
