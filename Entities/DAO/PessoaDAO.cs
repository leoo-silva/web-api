using MySql.Data.MySqlClient;
using web.Entities.Pessoa.Model;
using web.Entities.Factory;
using web.Entities.Pessoa.Model;


namespace web.Entities.Pessoa.DAO
{
    public class PessoaDAO
    {

        // método select between (busca pessoas entre as datas informadas)
        public List<Model.Pessoa> BuscandoPessoasEntreDatas(PessoaBetween pessoa)
        {
            List<Model.Pessoa> list;
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "select * from pessoas where data_nascimento between @start and @finish order by data_nascimento";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@start", pessoa.de.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@finish", pessoa.ate.ToString("yyyy-MM-dd"));

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            Model.Pessoa pessoaInfo;
                            list = new List<Model.Pessoa>();
                            while (reader.Read())
                            {
                                pessoaInfo = new Model.Pessoa();
                                pessoaInfo.Cpf = reader.GetString(0);
                                pessoaInfo.Nome = reader.GetString(1);
                                pessoaInfo.Profissao = reader.GetString(2);
                                pessoaInfo.Nacionalidade = reader.GetString(3);
                                pessoaInfo.DataNascimento = reader.GetDateTime(4);
                                pessoaInfo.Peso = reader.GetFloat(5);
                                pessoaInfo.Altura = reader.GetFloat(6);
                                pessoaInfo.Idade = pessoaInfo.DataNascimento.Year;

                                list.Add(pessoaInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Error: " + erro.Message);
            }

            return list;
        }

        // método select like (busca pela profissão)
        public List<Model.Pessoa> BuscandoProfissao(string profissao)
        {
            List<Model.Pessoa> list;
            Model.Pessoa pessoa;
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "select * from pessoas where profissao like ?";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("profissao", "%" + profissao.ToUpper() + "%");

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            list = new List<Model.Pessoa>();
                            while (reader.Read())
                            {
                                pessoa = new Model.Pessoa();
                                pessoa.Cpf = reader.GetString(0);
                                pessoa.Nome = reader.GetString(1);
                                pessoa.Profissao = reader.GetString(2);
                                pessoa.Nacionalidade = reader.GetString(3);
                                pessoa.DataNascimento = reader.GetDateTime(4);
                                pessoa.Peso = reader.GetFloat(5);
                                pessoa.Altura = reader.GetFloat(6);
                                pessoa.Idade = pessoa.DataNascimento.Year;

                                list.Add(pessoa);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Erro: " + erro.Message);
            }

            return list;
        }

        // método select cpf (busca os dados através do cpf)
        public Model.Pessoa BuscaPessoaCPF(string cpf)
        {
            Model.Pessoa pessoa;
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "select * from pessoas where cpf=?";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("cpf", cpf);

                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            pessoa = new Model.Pessoa();
                            if (reader.Read())
                            {
                                pessoa.Cpf = reader.GetString(0);
                                pessoa.Nome = reader.GetString(1);
                                pessoa.Profissao = reader.GetString(2);
                                pessoa.Nacionalidade = reader.GetString(3);
                                pessoa.DataNascimento = reader.GetDateTime(4);
                                pessoa.Peso = reader.GetFloat(5);
                                pessoa.Altura = reader.GetFloat(6);
                            }
                            else
                            {
                                throw new Exception("CPF não cadastrado.");
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                throw new Exception(erro.Message);
            }

            return pessoa;
        }

        // método select (lista todos as pessoas cadastradas)
        public List<Model.Pessoa> BuscaDados()
        {
            List<Model.Pessoa> list;
            Model.Pessoa pessoa;
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "select * from pessoas order by nome";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            list = new List<Model.Pessoa>();
                            while (reader.Read())
                            {
                                pessoa = new Model.Pessoa();
                                pessoa.Cpf = reader.GetString(0);
                                pessoa.Nome = reader.GetString(1);
                                pessoa.Profissao = reader.GetString(2);
                                pessoa.Nacionalidade = reader.GetString(3);
                                pessoa.DataNascimento = reader.GetDateTime(4);
                                pessoa.Peso = reader.GetFloat(5);
                                pessoa.Altura = reader.GetFloat(6);
                                pessoa.Idade = pessoa.DataNascimento.Year;

                                list.Add(pessoa);
                            }
                        }
                    }
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Error: " + erro.Message);
            }
            return list;
        }

        // método insert (insere dados digitados pelo usuário)
        public void InsereDados(Model.Pessoa pessoa)
        {
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "insert into pessoas" +
                        "(cpf, nome, profissao, nacionalidade, data_nascimento, peso, altura, idade)" +
                        "values" +
                        "(?, ?, ?, ?, ?, ?, ?, ?)";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("cpf", pessoa.Cpf);
                        cmd.Parameters.AddWithValue("nome", pessoa.Nome.ToUpper());
                        cmd.Parameters.AddWithValue("profissao", pessoa.Profissao.ToUpper());
                        cmd.Parameters.AddWithValue("nacionalidade", pessoa.Nacionalidade.ToUpper());
                        cmd.Parameters.AddWithValue("data_nascimento", pessoa.DataNascimento.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("peso", pessoa.Peso.ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("altura", pessoa.Altura.ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("idade", pessoa.Idade);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (MySqlException erro)
            {
                throw new Exception("O CPF digitado já está cadastrado.");
            }
            catch (Exception erro)
            {
                throw new Exception("" + erro);
            }
        }

        // método delete (deleta o cliente através do cpf)
        public void DeletaDados(string cpf)
        {
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "delete from pessoas where cpf=?";
                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("cpf", cpf);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Error: " + erro.Message);
            }
        }

        // método update (altera os dados já cadastrados)
        public void AlteraDados(Model.Pessoa pessoa)
        {
            try
            {
                using (var conn = ConnectionDB.GetConnection())
                {
                    string sql = "update pessoas set nome = ?, profissao = ?, nacionalidade = ?, data_nascimento = ?, peso = ?, altura = ?, idade = ? where cpf = ?";

                    using (var cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("nome", pessoa.Nome.ToUpper());
                        cmd.Parameters.AddWithValue("profissao", pessoa.Profissao.ToUpper());
                        cmd.Parameters.AddWithValue("nacionalidade", pessoa.Nacionalidade.ToUpper());
                        cmd.Parameters.AddWithValue("data_nascimento", pessoa.DataNascimento.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("peso", pessoa.Peso.ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("altura", pessoa.Altura.ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("idade", pessoa.Idade);
                        cmd.Parameters.AddWithValue("cpf", pessoa.Cpf);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception erro)
            {
                throw new Exception("Error: " + erro.Message);
            }
        }
    }
}
