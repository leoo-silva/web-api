using MySql.Data.MySqlClient;
using web.Entities.PException;


namespace web.Entities.Factory
{
    public class ConnectionFactory
    {
        private static string url = "server=localhost;database=web;user=root;password=root123";
        private static MySqlConnection conn;

        public static MySqlConnection GetConnection()
        {
            try
            {
                conn = new MySqlConnection(url);
                conn.Open();
            }
            catch (Exception erro)
            {
                throw new PessoaException("Error: " + erro.Message);
            }

            return conn;
        }
    }
}
