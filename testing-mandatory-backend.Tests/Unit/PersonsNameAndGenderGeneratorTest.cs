using testing_mandatory_backend.Services;

[Trait("Category", "Unit")]
public class PersonServiceTests
{
    [Fact]
    public void GetPersonFromJsonFile_ShouldReturnCorrectNumberOfPersons_WithMatchingGender()
    {
        var personService = new PersonService();
 
        var result = personService.GetPersonFromJsonFile("Male", 2);

        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal("Male", p.Gender, StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public void GetPersonFromJsonFile_ShouldReturnRandomPersons()
    {
        var personService = new PersonService();

        var result = personService.GetPersonFromJsonFile("Male", 2);

        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal("Male", p.Gender, StringComparer.OrdinalIgnoreCase));
    }


}
