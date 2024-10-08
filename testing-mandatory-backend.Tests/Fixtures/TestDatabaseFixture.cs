using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

[CollectionDefinition("Database Collection")]
public class DatabaseCollection : ICollectionFixture<TestDatabaseFixture>
{
}

public class TestDatabaseFixture : IDisposable
{
    public MySqlConnection Connection { get; private set; }

    public TestDatabaseFixture()
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: false)
            .Build();

        string? connectionString = config.GetConnectionString("test");
        Connection = new MySqlConnection(connectionString);
        Connection.Open();
    }

    public void ResetDatabase()
    {
        var clearCommand = new MySqlCommand("DELETE FROM postal_code;", Connection);
        clearCommand.ExecuteNonQuery();
    }

    public void SeedDatabase(params (string? postalCode, string? townName)[] entries)
    {
        foreach (var (postalCode, townName) in entries)
        {
            var insertCommand = new MySqlCommand(
                $"INSERT INTO postal_code (cPostalCode, cTownName) VALUES ('{postalCode ?? null}', '{townName ?? null}');",
                Connection);
            insertCommand.ExecuteNonQuery();
        }
    }

    public void Dispose()
    {
        Connection.Close();
    }
}
