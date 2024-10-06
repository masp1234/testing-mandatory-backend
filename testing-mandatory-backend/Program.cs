
using MySql.Data.MySqlClient;
using testing_mandatory_backend.Repositories;

namespace testing_mandatory_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Add scoped MySQL connection (new db connection for each request)
            builder.Services.AddScoped<MySqlConnection>(serviceProvider =>
            {
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                string? connectionString = config.GetConnectionString("dev");
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
    }
}
