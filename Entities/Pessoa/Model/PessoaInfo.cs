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
    }
}
