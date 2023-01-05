using MySql.Data.MySqlClient;
using web.Entities.Pessoa.Model;
using web.Entities.Factory;
using web.Entities.PException;
using web.Entities.Pessoa.Model;


namespace web.Entities.Pessoa.DAO
{
    public class PessoaDAO
    {
        //fazer commit "Criação de funcionalidade pesquisa por data"
        //fazer testes e revisar site
        private MySqlConnection conn;
        private MySqlCommand cmd;
        private PessoaInfo pessoa;
        private List<PessoaInfo> list;

        // método select between (busca pessoas entre as datas informadas)
        public List<PessoaInfo> SelectBetween(PessoaBetween pessoa)
        {
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "select * from pessoas where data_nascimento between @start and @finish order by data_nascimento";

                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@start", pessoa.GetDeUS());
                        cmd.Parameters.AddWithValue("@finish", pessoa.GetAteUS());

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            PessoaInfo pessoaInfo;
                            this.list = new List<PessoaInfo>();
                            while (reader.Read())
                            {
                                pessoaInfo = new PessoaInfo();
                                pessoaInfo.SetCpf(reader.GetString(0));
                                pessoaInfo.SetNome(reader.GetString(1));
                                pessoaInfo.SetProfissao(reader.GetString(2));
                                pessoaInfo.SetNacionalidade(reader.GetString(3));
                                pessoaInfo.SetDataNascimento(reader.GetDateTime(4));
                                pessoaInfo.SetPeso(reader.GetFloat(5));
                                pessoaInfo.SetAltura(reader.GetFloat(6));
                                pessoaInfo.SetIdade(reader.GetDateTime(4));

                                this.list.Add(pessoaInfo);
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

            return this.list;
        }

        // método select like (busca pela profissão)
        public List<PessoaInfo> SelectLike(string profissao)
        {
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "select * from pessoas where profissao like ?";

                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("profissao", "%" + profissao.ToUpper() + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            this.list = new List<PessoaInfo>();
                            while (reader.Read())
                            {
                                this.pessoa = new PessoaInfo();
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
                throw new PessoaException("Erro: " + erro.Message);
            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }

            return this.list;
        }

        // método select cpf (busca os dados através do cpf)
        public PessoaInfo SelectCPF(string cpf)
        {
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
                            pessoa = new PessoaInfo();
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
                            else
                            {
                                throw new PessoaException("CPF não cadastrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                throw new PessoaException(erro.Message);
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
            try
            {
                using (conn = ConnectionFactory.GetConnection())
                {
                    string sql = "select * from pessoas order by nome";
                    using (cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            this.list = new List<PessoaInfo>();
                            while (reader.Read())
                            {
                                this.pessoa = new PessoaInfo();
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
            return this.list;
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
