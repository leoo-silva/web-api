using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;
using web.Entities.Factory;
using web.Entities.PException;
using System.Threading.Tasks;


namespace web.Pages
{
    public class CadastrarModel : PageModel
    {
        public PessoaInfo pessoa = new PessoaInfo();
        private PessoaDAO dao;
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                pessoa.SetCpf(Request.Form["cpf"]);
                pessoa.SetNome(Request.Form["nome"]);
                pessoa.SetProfissao(Request.Form["profissao"]);
                pessoa.SetNacionalidade(Request.Form["nacionalidade"]);
                pessoa.SetDataNascimento(DateTime.Parse(Request.Form["dataNascimento"]));
                try
                {
                    pessoa.SetPeso(float.Parse(Request.Form["peso"]));
                    pessoa.SetAltura(float.Parse(Request.Form["altura"]));
                }
                catch (FormatException erro)
                {
                    this.errorMessage = "É necessário digitar um valor numérico separados por VÍRGULA para os campos de Peso e Altura.";
                    return;
                }
            }
            catch (FormatException erro)
            {
                this.errorMessage = "Todos os campos devem ser preenchidos.";
                return;
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }

            try
            {
                if (pessoa.ValidaCPF())
                {
                    errorMessage = "CPF Inválido";
                    return;
                }
            }
            catch (ArgumentOutOfRangeException erro)
            {
                this.errorMessage = "O CPF precisa ser preenchido com valores numéricos.";
                return;
            }
            catch (Exception erro)
            {
                errorMessage = erro.Message;
                return;
            }

            if (pessoa.DataMaior())
            {
                this.errorMessage = "A data de nascimento não pode ser maior que a data atual";
                return;
            }

            if (pessoa.LengthCampos())
            {
                this.errorMessage = "Algum campo não foi preenchido ou excedeu seu limite de caracteres. Tente Novamente.";
                return;
            }


            // salvando dados no banco caso não tenha erros
            try
            {
                dao = new PessoaDAO();
                dao.InsertInto(pessoa);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }
            this.successMessage = "Pessoa Cadastrada com Sucesso.";
            // fazer commit (Criação da funcionalidade - cadastrar pessoa)
        }
    }
}
