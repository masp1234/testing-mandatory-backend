// File: Program.cs

using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using MySql.Data.MySqlClient;

namespace testing_mandatory_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Load environment variables depending on the current environment
            var env = builder.Environment.EnvironmentName;
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                // This will load appsettings.Test.json when in Test environment
                .AddJsonFile($"appsettings.{env}.json", optional: true)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Add services to the container.

            // Add scoped MySQL connection (new db connection for each request)
            builder.Services.AddScoped<MySqlConnection>(serviceProvider =>
            {
                string? connectionString = config.GetConnectionString(env == "Test" ? "TestDatabase" : "dev");
                var connection = new MySqlConnection(connectionString);
                connection.Open();
                return connection;
            });

            // Register repositories and services
            builder.Services.AddScoped<IPostalCodeRepository, PostalCodeRepository>();
            builder.Services.AddScoped<FakeAddressGenerator>();
            builder.Services.AddScoped<INameAndGenderRepository, NameAndGenderRepository>();
            builder.Services.AddScoped<NameAndGenderGenerator>();

            // Register the PhoneNumberGenerator service
            builder.Services.AddScoped<IPhoneNumberGenerator, PhoneNumberGenerator>();

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
    }
}