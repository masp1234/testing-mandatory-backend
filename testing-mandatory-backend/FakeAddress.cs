namespace testing_mandatory_backend
{
    public class FakeAddress
    {
        public string Street { get; }
        public string Number { get; }
        public string Door { get; }
        public string Floor { get; }
        public string PostalCode { get; }
        public string TownName { get; }

        public FakeAddress(string street, string number, string door, string floor, string postalCode, string townName)
        {
            Street = street;
            Number = number;
            Door = door;
            Floor = floor;
            PostalCode = postalCode;
            TownName = townName;
        }
    }
}
