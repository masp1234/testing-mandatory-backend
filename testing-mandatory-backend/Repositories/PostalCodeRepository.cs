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
            List<(string postalCode, string townName)> postalCodes = [];

            MySqlCommand command = new("SELECT * FROM postal_code", this.connection);

            try
            {
                MySqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    postalCodes.Add((reader.GetString(0), reader.GetString(1)));

                }
                reader.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
            return postalCodes;

        }
    }
}
