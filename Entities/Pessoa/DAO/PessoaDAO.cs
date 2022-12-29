using MySql.Data.MySqlClient;
using web.Entities.Pessoa.Model;
using web.Entities.Factory;
using web.Entities.PException;


namespace web.Entities.Pessoa.DAO
{
    public class PessoaDAO
    {
        private MySqlConnection conn;
        private MySqlCommand cmd;

        // método select (lista todos as pessoas cadastradas)
        public List<PessoaInfo> Select()
        {
            List<PessoaInfo> list;
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "select * from pessoas";
                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            list = new List<PessoaInfo>();
                            while (reader.Read())
                            {
                                PessoaInfo pessoa = new PessoaInfo();
                                pessoa.SetCpf(reader.GetString(0));
                                pessoa.SetNome(reader.GetString(1));
                                pessoa.SetProfissao(reader.GetString(2));
                                pessoa.SetNacionalidade(reader.GetString(3));
                                pessoa.SetDataNascimento(reader.GetDateTime(4));
                                pessoa.SetPeso(reader.GetFloat(5));
                                pessoa.SetAltura(reader.GetFloat(6));
                                pessoa.SetIdade(reader.GetDateTime(4));

                                list.Add(pessoa);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                throw new PessoaException("Error: " + erro.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
            return list;
        }
    }
}
