namespace testing_mandatory_backend.Tests.Fixtures
{
    public class BirthdayGeneratorFixture {
        public List<DateTime> RandomBirthdays { get; private set; } // Can be used outside its class, but only modified within.
        public const int numberOfTests = 1000;

        public BirthdayGeneratorFixture() {
            RandomBirthdays = new List<DateTime>();
            for (int i = 0; i < numberOfTests; i++) {
                RandomBirthdays.Add(BirthdayGenerator.GenerateRandomBirthday());
            }
        }
    }
}