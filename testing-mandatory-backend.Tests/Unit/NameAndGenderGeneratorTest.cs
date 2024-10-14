using Moq;
using testing_mandatory_backend.Models;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using Xunit;
using System.Text.Json;

[Trait("Category", "Unit")]
public class NameAndGenderGeneratorTests
{


    [Fact]
    public void GetNameAndGenderFromJsonFile_ShouldReturnRandomPerson()
    {
        var mockRepo = new Mock<INameAndGenderRepository>();
        mockRepo.Setup(repo => repo.GetNameAndGender()).Returns(new List<Person>
        {
            new("John",  "Doe", "Male") ,
            new ("Jane", "Doe", "Female" ),
        });

        var generator = new NameAndGenderGenerator(mockRepo.Object);

        var result = generator.GenerateNameAndGender();

        Assert.NotNull(result);
    }


    [Fact]
    public void GetNameAndGenderFromJsonFile_ShouldThrowException_IfNoPersonsInRepository()
    {
        var mockRepo = new Mock<INameAndGenderRepository>();
        mockRepo.Setup(repo => repo.GetNameAndGender()).Returns(new List<Person>()); // Empty list

        var generator = new NameAndGenderGenerator(mockRepo.Object);

        var exception = Assert.Throws<Exception>(() => generator.GenerateNameAndGender());
        Assert.Equal("No persons found in the JSON data.", exception.Message);
    }

    [Fact]
    public void GetNameAndGenderFromJsonFile_ShouldReturnRandomPersonFromList()
    {
        var mockRepo = new Mock<INameAndGenderRepository>();
        var mockPersons = new List<Person>
        {
            new  ("John", "Doe", "Male"),
            new  ("Michael", "Smith", "Male"),
            new  ("David", "Johnson", "Male")
        };
        
        mockRepo.Setup(repo => repo.GetNameAndGender()).Returns(mockPersons);

        var generator = new NameAndGenderGenerator(mockRepo.Object);

        var result = generator.GenerateNameAndGender();

        Assert.NotNull(result);  // Ensure we get a person
        Assert.Contains(result, mockPersons);  // Ensure the returned person is in the mocked list
    }
    [Fact]
    public void GetNameAndGender_ShouldReturnCorrectData_FromJsonFile()
    {
            var repository = new NameAndGenderRepository();

            var generator = new NameAndGenderGenerator(repository);

            var result = generator.GenerateNameAndGender();

            Assert.NotNull(result);  // Check that a person is returned
    }

}
