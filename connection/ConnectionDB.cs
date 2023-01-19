using MySql.Data.MySqlClient;


namespace web.Entities.Factory
{
    public class ConnectionDB
    {
        private static string url = "server=sql10.freemysqlhosting.net;database=sql10591615;user=sql10591615;password=fcpIg8jjEl;port=3306";
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
                throw new Exception("Error: " + erro.Message);
            }

            return conn;
        }
    }
}
