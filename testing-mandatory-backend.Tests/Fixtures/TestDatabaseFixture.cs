using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

public class TestDatabaseFixture
{
    private readonly string? connectionString;
    public MySqlConnection Connection;

    public TestDatabaseFixture()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: false)
            .Build();

        connectionString = config.GetConnectionString("test");
        Connection = new MySqlConnection(connectionString);
    }

    public void CreateNewConnection()
    {
        Connection = new MySqlConnection(connectionString);
        Connection.Open();
    }

    public void ResetDatabase()
    {
        var clearCommand = new MySqlCommand("DELETE FROM postal_code;", this.Connection);
        clearCommand.ExecuteNonQuery();
    }

    public void SeedDatabase(params (string? postalCode, string? townName)[] entries)
    {
        foreach (var (postalCode, townName) in entries)
        {
            var insertCommand = new MySqlCommand(
                $"INSERT INTO postal_code (cPostalCode, cTownName) VALUES ('{postalCode}', '{townName}');",
                this.Connection);
            insertCommand.ExecuteNonQuery();
        }
    }
}
