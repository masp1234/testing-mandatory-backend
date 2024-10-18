using testing_mandatory_backend.Models;
using Xunit;

namespace testing_mandatory_backendTests.Util
{
    public static class PersonDataValidator
    {
        public static void CheckForNullValues(PersonData personData)
        {
            Assert.NotNull(personData.FakeAddress);
            Assert.NotNull(personData.Person);
            /*
             Check that Birthday has actually been set,
             and that it is not using the default value for DateTime (which is DateTime.MinValue)
            */
            Assert.NotEqual(DateTime.MinValue, personData.BirthDate);
            Assert.False(string.IsNullOrEmpty(personData.PhoneNumber));
            Assert.False(string.IsNullOrEmpty(personData.CPR));
        }
    }
}
