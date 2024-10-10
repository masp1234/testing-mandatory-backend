
namespace testing_mandatory_backend;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using MySql.Data.MySqlClient;


public class Program
{
    public static void Main(string[] args)
    {
        PhoneNumberGenerator generator = new PhoneNumberGenerator();
        try
        {
            // Generate a phone number with a specific prefix
            Console.WriteLine("Generated Phone Number: " + generator.GeneratePhoneNumber("342"));

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Load environment variables depending on the current environment
            var env = builder.Environment.EnvironmentName;
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // This will load appsettings.Test.json when in Test environment
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add scoped MySQL connection (new db connection for each request)
            builder.Services.AddScoped<MySqlConnection>(serviceProvider =>
            {
                string? connectionString = config.GetConnectionString(env == "Test" ? "TestDatabase" : "dev");
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            });

            // Add inject PostalCodeRepository wherever it is needed
            builder.Services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();

            builder.Services.AddScoped<FakeAddressGenerator>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (ArgumentException ex)
        {
            // Handle invalid prefix error
            Console.WriteLine("Error: " + ex.Message);
        }

        // Generate multiple phone numbers in bulk
        List<string> bulkNumbers = generator.GenerateBulkPhoneNumbers(10);
        Console.WriteLine("Bulk Generated Numbers: ");
        bulkNumbers.ForEach(Console.WriteLine); // Print each generated phone number
    }
}
