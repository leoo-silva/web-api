using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;
using web.Entities.Factory;
using System.Threading.Tasks;


namespace web.Pages
{
    public class CadastrarModel : PageModel
    {
        public Pessoa pessoa = new Pessoa();
        private PessoaDAO dao;
        public string errorMessage = "";

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                pessoa.Cpf = Request.Form["cpf"];
                pessoa.Nome = Request.Form["nome"];
                pessoa.Profissao = Request.Form["profissao"];
                pessoa.Nacionalidade = Request.Form["nacionalidade"];
                pessoa.DataNascimento = DateTime.Parse(Request.Form["dataNascimento"]);
                pessoa.Peso = float.Parse(Request.Form["peso"]);
                pessoa.Altura = float.Parse(Request.Form["altura"]);
            }
            catch (FormatException erro)
            {
                this.errorMessage = "Todos os campos devem ser preenchidos corretamente.";
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
                    errorMessage = "CPF inv�lido.";
                    return;
                }
            }
            catch (ArgumentOutOfRangeException erro)
            {
                this.errorMessage = "O CPF precisa ser preenchido com valores num�ricos.";
                return;
            }
            catch (Exception erro)
            {
                errorMessage = erro.Message;
                return;
            }

            if (pessoa.ValidaDataNascimento())
            {
                this.errorMessage = "A data de nascimento n�o pode ser maior que a data atual";
                return;
            }

            if (pessoa.TamanhoCampos() || pessoa.TamanhoCPF())
            {
                this.errorMessage = "Algum campo n�o foi preenchido ou excedeu seu limite de caracteres. Tente Novamente.";
                return;
            }

            if (pessoa.DataNascimento.ToString("yyyy-MM-dd") == "0001-01-01")
            {
                this.errorMessage = "Preencha a data de nascimento corretamente.";
                return;
            }


            // salvando dados no banco caso n�o tenha erros
            try
            {
                dao = new PessoaDAO();
                dao.InsereDados(pessoa);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }

            Response.Redirect("/Listagem");
        }
    }
}
