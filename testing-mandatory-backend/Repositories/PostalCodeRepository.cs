using MySql.Data.MySqlClient;

namespace testing_mandatory_backend.Repositories
{
    public class PostalCodeRepository: IPostalCodeRepository
    {
        private readonly MySqlConnection connection;

        public PostalCodeRepository(MySqlConnection connection)
        {
            this.connection = connection;
        }

        public List<(string postalCode, string townName)> GetPostalCodesAndTowns()
        {
            MySqlCommand command = new("SELECT * FROM postal_code", this.connection);

            MySqlDataReader reader = command.ExecuteReader();

            List<(string postalCode, string townName)> postalCodes = [];

            while (reader.Read())
            {
                postalCodes.Add((reader.GetString(0), reader.GetString(1)));

            }
            reader.Close();
            return postalCodes;

        }
    }
}
