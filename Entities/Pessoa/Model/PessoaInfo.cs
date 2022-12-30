using web.Entities.PException;


namespace web.Entities.Pessoa.Model
{
    public class PessoaInfo
    {
        private DateTime dataAtual = DateTime.Now;
        private string cpf;
        private string nome;
        private string profissao;
        private string nacionalidade;
        private DateTime dataNascimento;
        private float peso;
        private float altura;
        private int idade;

        public bool LengthCampos()
        {
            if (this.LengthCpf() || this.LengthNome() || this.LengthProfissao() || this.LengthNacionalidade() || this.peso == 0 || this.altura == 0)
            {
                return true;
            }
            return false;
        }

        public bool DataMaior()
        {
            if (this.dataNascimento >= this.dataAtual)
            {
                return true;
            }
            return false;
        }

        private bool LengthNacionalidade()
        {
            if (this.nacionalidade.Length < 3 || this.nacionalidade.Length > 30)
            {
                return true;
            }
            return false;
        }

        private bool LengthProfissao()
        {
            if (this.profissao.Length < 3 || this.profissao.Length > 50)
            {
                return true;
            }
            return false;
        }

        private bool LengthNome()
        {
            if (this.nome.Length < 5 || this.nome.Length > 50)
            {
                return true;
            }
            return false;
        }

        private bool LengthCpf()
        {
            if (this.cpf.Length < 11 || this.cpf.Length > 11)
            {
                return true;
            }
            return false;
        }

        public int GetIdade()
        {
            return this.dataAtual.Year - this.dataNascimento.Year;
        }

        public string GetCpf()
        {
            return this.cpf;
        }

        public string GetNome()
        {
            return this.nome;
        }

        public string GetProfissao()
        {
            return this.profissao;
        }

        public string GetNacionalidade()
        {
            return this.nacionalidade;
        }

        // método para salvar data no banco de dados (formato americano yyyy-MM-dd)
        public string GetDataNascimentoUS()
        {
            return this.dataNascimento.ToString("yyyy-MM-dd");
        }

        // método para salvar data no banco de dados (formato brasileiro dd/MM/yyyy)
        public string GetDataNascimento()
        {
            return this.dataNascimento.ToString("dd/MM/yyyy");
        }

        public float GetPeso()
        {
            return this.peso;
        }

        public float GetAltura()
        {
            return this.altura;
        }

        public void SetCpf(string cpf)
        {
            this.cpf = cpf;
        }

        public void SetNome(string nome)
        {
            this.nome = nome;
        }

        public void SetProfissao(string profissao)
        {
            this.profissao = profissao;
        }

        public void SetNacionalidade(string nacionalidade)
        {
            this.nacionalidade = nacionalidade;
        }

        public void SetDataNascimento(DateTime dataNascimento)
        {
            this.dataNascimento = dataNascimento;
        }

        public void SetPeso(float peso)
        {
            this.peso = peso;
        }

        public void SetAltura(float altura)
        {
            this.altura = altura;
        }

        public void SetIdade(DateTime valor)
        {
            int ano = valor.Year;
            this.idade = this.dataAtual.Year - ano;
        }

        public string FormatCPF()
        {
            int v = 0;
            string newCpf = "";
            
            for (int count=0; count < this.GetCpf().Length; count++)
            {
                newCpf += this.GetCpf()[count];
                v++;
                if (v == 3)
                {
                    if (count == 8)
                    {
                        newCpf += "-";
                        continue;
                    }
                    newCpf += ".";
                    v = 0;
                }
            }

            return newCpf;
        }

        public bool ValidaCPF()
        {
            for(int count = 0; count < this.cpf.Length; count++)
            {
                if (this.cpf[count].ToString() == "-" || this.cpf[count] == '/')
                {
                    throw new PessoaException("Digite somente os números do seu CPF.");
                }
            }

            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string newCpf = this.GetCpf().Substring(0,9);
            int total = 0;
            int resto;
            string digito;

            for (int count = 0; count < 9; count++)
            {
                total += int.Parse(newCpf[count].ToString()) * mult1[count];
            }
            resto = total % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            newCpf += digito;
            total = 0;

            for (int count = 0; count < 10; count++)
            {
                total += int.Parse(newCpf[count].ToString()) * mult2[count];
            }
            resto = total % 11;
            if (resto < 2)
            {
                resto = 0;
            }
            else
            {
                resto = 11 - resto;
            }
            digito = resto.ToString();
            newCpf += digito;

            if (!(this.cpf == newCpf))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
