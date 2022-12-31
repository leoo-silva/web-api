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

        // método select cpf (busca os dados através do cpf)
        public PessoaInfo SelectCPF(string cpf)
        {
            PessoaInfo pessoa = new PessoaInfo();
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "select * from pessoas where cpf=?";
                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("cpf", cpf);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                pessoa.SetCpf(reader.GetString(0));
                                pessoa.SetNome(reader.GetString(1));
                                pessoa.SetProfissao(reader.GetString(2));
                                pessoa.SetNacionalidade(reader.GetString(3));
                                pessoa.SetDataNascimento(reader.GetDateTime(4));
                                pessoa.SetPeso(reader.GetFloat(5));
                                pessoa.SetAltura(reader.GetFloat(6));
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

            return pessoa;
        }

        // método select (lista todos as pessoas cadastradas)
        public List<PessoaInfo> Select()
        {
            List<PessoaInfo> list;
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "select * from pessoas order by nome";
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

        // método insert (insere dados digitados pelo usuário)
        public void InsertInto(PessoaInfo pessoa)
        {
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "insert into pessoas" +
                        "(cpf, nome, profissao, nacionalidade, data_nascimento, peso, altura, idade)" +
                        "values" +
                        "(?, ?, ?, ?, ?, ?, ?, ?)";
                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("cpf", pessoa.GetCpf());
                        cmd.Parameters.AddWithValue("nome", pessoa.GetNome().ToUpper());
                        cmd.Parameters.AddWithValue("profissao", pessoa.GetProfissao().ToUpper());
                        cmd.Parameters.AddWithValue("nacionalidade", pessoa.GetNacionalidade().ToUpper());
                        cmd.Parameters.AddWithValue("data_nascimento", pessoa.GetDataNascimentoUS());
                        cmd.Parameters.AddWithValue("peso", pessoa.GetPeso().ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("altura", pessoa.GetAltura().ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("idade", pessoa.GetIdade());
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException erro)
            {
                throw new PessoaException("O CPF digitado já está cadastrado.");
            }
            catch (Exception erro)
            {
                throw new PessoaException("" + erro);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }
        }

        // método delete (deleta o cliente através do cpf)
        public void Delete(string cpf)
        {
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "delete from pessoas where cpf=?";
                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("cpf", cpf);

                        cmd.ExecuteNonQuery();
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
        }

        // método update (altera os dados já cadastrados)
        public void Update(PessoaInfo pessoa)
        {
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "update pessoas set nome = ?, profissao = ?, nacionalidade = ?, data_nascimento = ?, peso = ?, altura = ?, idade = ? where cpf = ?";

                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("nome", pessoa.GetNome().ToUpper());
                        cmd.Parameters.AddWithValue("profissao", pessoa.GetProfissao().ToUpper());
                        cmd.Parameters.AddWithValue("nacionalidade", pessoa.GetNacionalidade().ToUpper());
                        cmd.Parameters.AddWithValue("data_nascimento", pessoa.GetDataNascimentoUS());
                        cmd.Parameters.AddWithValue("peso", pessoa.GetPeso().ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("altura", pessoa.GetAltura().ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("idade", pessoa.GetIdade());
                        cmd.Parameters.AddWithValue("cpf", pessoa.GetCpf());

                        cmd.ExecuteNonQuery();
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
        }
    }
}
