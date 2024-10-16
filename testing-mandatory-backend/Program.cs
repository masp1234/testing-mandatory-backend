// File: Program.cs

using testing_mandatory_backend.Repositories;
using testing_mandatory_backend.Services;
using MySql.Data.MySqlClient;
using testing_mandatory_backend.Middleware; // Namespace for ExceptionHandlingMiddleware

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

// Register controllers
builder.Services.AddControllers();

// Configure Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// **CORS Configuration**
// Define a CORS policy named "AllowedOrigins"
var allowedOrigins = "AllowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowedOrigins,
        policy =>
        {
            policy.WithOrigins("http://localhost:5173") // React frontend URL
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

// **Use Swagger in Development**
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// **Use HTTPS Redirection**
app.UseHttpsRedirection();

// **Use CORS**
app.UseCors(allowedOrigins);

// **Use Global Exception Handling Middleware**
app.UseMiddleware<ExceptionHandlingMiddleware>();

// **Use Authorization**
app.UseAuthorization();

// **Map Controllers**
app.MapControllers();

// **Run the Application**
app.Run();