
namespace testing_mandatory_backend;
using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Cors;



public class Program
{
    public static void Main(string[] args)
    {
        try
        {
        
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

            // Register repositories
            builder.Services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
            builder.Services.AddScoped<INameAndGenderRepository, NameAndGenderRepository>();

            // Register services
            builder.Services.AddScoped<INameAndGenderGenerator, NameAndGenderGenerator>();
            builder.Services.AddScoped<IFakeAddressGenerator, FakeAddressGenerator>();
            builder.Services.AddScoped<IPhoneNumberGenerator, PhoneNumberGenerator>();
            builder.Services.AddScoped<IBirthdayGenerator, BirthdayGenerator>();
            builder.Services.AddScoped<ICprGenerator, CprGenerator>();
            builder.Services.AddScoped<IPersonDataService, PersonDataService>();
            builder.Services.AddScoped<IPersonDataService, PersonDataService>();

            builder.Services.AddControllers();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", builder =>
                {
                    builder.AllowAnyOrigin() // Allows all origins
                           .AllowAnyMethod() // Allows all HTTP methods
                           .AllowAnyHeader(); // Allows all headers
                });
            });

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
            app.UseCors("AllowAllOrigins");
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
        catch (ArgumentException ex)
        {
            // Handle invalid prefix error
            Console.WriteLine("Error: " + ex.Message);
        }

       
    }
}
