namespace web.Entities.Pessoa.Model
{
    public class Pessoa
    {
        private DateTime _dataAtual = DateTime.Now;
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public string Profissao { get; set; }
        public string Nacionalidade { get; set; }
        public DateTime DataNascimento { get; set; }
        public float Peso { get; set; }
        public float Altura { get; set; }
        public int Idade
        {
            get
            {
                return _dataAtual.Year - DataNascimento.Year;
            }

            set
            {
                int ano = DataNascimento.Year;
                Idade = _dataAtual.Year - DataNascimento.Year;
            }
        }

        public bool TamanhoCampos()
        {
            if (this.TamanhoNome() || this.TamanhoProfissao() || this.TamanhoNacionalidade() || this.Peso == 0 || this.Altura == 0)
            {
                return true;
            }
            return false;
        }

        public bool ValidaDataNascimento()
        {
            if (this.DataNascimento >= this._dataAtual)
            {
                return true;
            }
            return false;
        }

        private bool TamanhoNacionalidade()
        {
            if (this.Nacionalidade.Length < 3 || this.Nacionalidade.Length > 30)
            {
                return true;
            }
            return false;
        }

        private bool TamanhoProfissao()
        {
            if (this.Profissao.Length < 3 || this.Profissao.Length > 50)
            {
                return true;
            }
            return false;
        }

        private bool TamanhoNome()
        {
            if (this.Nome.Length < 5 || this.Nome.Length > 50)
            {
                return true;
            }
            return false;
        }

        public bool TamanhoCPF()
        {
            if (!(this.Cpf.Length == 11))
            {
                return true;
            }
            return false;
        }

        public string FormatCPF()
        {
            int v = 0;
            string newCpf = "";
            
            for (int count=0; count < this.Cpf.Length; count++)
            {
                newCpf += this.Cpf[count];
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
            for(int count = 0; count < this.Cpf.Length; count++)
            {
                if (this.Cpf[count].ToString() == "-" || this.Cpf[count] == '/')
                {
                    throw new Exception("Digite somente os números do CPF.");
                }
            }

            int[] mult1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            string newCpf = this.Cpf.Substring(0,9);
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

            if (!(this.Cpf == newCpf && this.ConfereSequencia(newCpf)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool ConfereSequencia(string cpf)
        {
            switch (cpf)
            {
                case "11111111111":
                    return false;
                case "00000000000":
                    return false;
                case "2222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;
                default:
                    return true;
            }
        }
    }
}
