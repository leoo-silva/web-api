namespace web.Entities.Pessoa.Model
{
    public class PessoaInfo
    {
        private string cpf;
        private string nome;
        private string profissao;
        private string nacionalidade;
        private DateTime dataNascimento;
        private float peso;
        private float altura;

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

        public DateTime GetDataNascimento()
        {
            return this.dataNascimento;
        }

        public float GetPeso()
        {
            return this.peso;
        }

        public float GetAltura()
        {
            return this.altura;
        }
    }
}
