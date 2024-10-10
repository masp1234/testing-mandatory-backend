using testing_mandatory_backend.Services;
using testing_mandatory_backend.Models;

using System.Text.Json;
using Xunit;

public class PersonServiceTests
{
    [Fact]
    public void GetPersonFromJsonFile_ShouldReturnCorrectNumberOfPersons_WithMatchingGender()
    {
        var personService = new PersonService();
        var mockPersonsData = new List<Person>
        {
            new("John","Doe","Male"),
            new( "Jane",  "Doe",  "Female" ),
            new("Michael", "Smith", "Male"),
            new("Emily", "Jones", "Female")
        };

        var json = JsonSerializer.Serialize(mockPersonsData);
        File.WriteAllText("./Data/person-names.json", json);

        var result = personService.GetPersonFromJsonFile("Male", 2);

        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal("Male", p.Gender, StringComparer.OrdinalIgnoreCase));
    }

    [Fact]
    public void GetPersonFromJsonFile_ShouldThrowException_IfNoPersonsExist()
    {
        var personService = new PersonService();

        File.WriteAllText("./Data/person-names.json", "[]");

        var exception = Assert.Throws<Exception>(() => personService.GetPersonFromJsonFile("Male", 2));
        Assert.Equal("No persons found in the JSON data.", exception.Message);
    }

    [Fact]
    public void GetPersonFromJsonFile_ShouldThrowException_IfNoPersonsWithMatchingGender()
    {
        var personService = new PersonService();
        var mockPersonsData = new List<Person>
        {
            new("John", "Doe", "Male"),
            new("Michael", "Smith", "Male")
        };

        var json = JsonSerializer.Serialize(mockPersonsData);
        File.WriteAllText("./Data/person-names.json", json);

        var exception = Assert.Throws<Exception>(() => personService.GetPersonFromJsonFile("Female", 2));
        Assert.Equal("No persons found with gender 'Female'.", exception.Message);
    }

    [Fact]
    public void GetPersonFromJsonFile_ShouldReturnRandomPersons()
    {
        var personService = new PersonService();
        var mockPersonsData = new List<Person>
        {
            new("John", "Doe", "Male"),
            new("Michael", "Smith", "Male"),
            new("David", "Johnson", "Male")
        };

        var json = JsonSerializer.Serialize(mockPersonsData);
        File.WriteAllText("./Data/person-names.json", json);

        var result = personService.GetPersonFromJsonFile("Male", 2);

        Assert.Equal(2, result.Count);
        Assert.All(result, p => Assert.Equal("Male", p.Gender, StringComparer.OrdinalIgnoreCase));
    }


}
