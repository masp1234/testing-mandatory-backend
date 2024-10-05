using MySql.Data.MySqlClient;

namespace testing_mandatory_backend.Database
{
    public static class DbConnection
    {
        private static MySqlConnection? connection;

        public static MySqlConnection GetConnection()
        {
            if (connection == null)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

                IConfiguration config = builder.Build();
                string connectionString = config.GetConnectionString("dev");

                connection = new MySqlConnection(connectionString);
                connection.Open();
            }

            return connection;
        }

    }
}
