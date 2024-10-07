public class BirthdayGeneratorTests
{
    [Fact]
    public void GenerateRandomBirthday_ShouldReturnDateWithinValidRange()
    {
        // Arrange
        DateTime today = DateTime.Today;
        DateTime earliestDate = today.AddYears(-100);
        DateTime latestDate = today.AddYears(-18);

        // Act
        DateTime randomBirthday = BirthdayGenerator.GenerateRandomBirthday();

        // Assert
        Assert.InRange(randomBirthday, earliestDate, latestDate);
    }

    [Fact]
    public void GenerateRandomBirthday_ShouldNotReturnFutureDate()
    {
        // Act
        DateTime randomBirthday = BirthdayGenerator.GenerateRandomBirthday();

        // Assert
        Assert.True(randomBirthday <= DateTime.Today);
    }

    [Fact]
    public void GenerateRandomBirthday_ShouldNotReturnDateLessThan100YearsAgo()
    {
        // Act
        DateTime randomBirthday = BirthdayGenerator.GenerateRandomBirthday();

        // Assert
        Assert.True(randomBirthday >= DateTime.Today.AddYears(-100));
    }

    [Fact]
    public void GenerateRandomBirthday_ShouldNotReturnDateLessThan18YearsAgo()
    {
        // Act
        DateTime randomBirthday = BirthdayGenerator.GenerateRandomBirthday();

        // Assert
        Assert.True(randomBirthday <= DateTime.Today.AddYears(-18));
    }
}