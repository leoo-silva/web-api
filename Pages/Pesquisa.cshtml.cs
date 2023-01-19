using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.DAO;
using web.Entities.Pessoa.Model;


namespace web.Pages
{
    public class PesquisaModel : PageModel
    {
        public Pessoa pessoa = new Pessoa();
        public string errorMessage = "";
        public string successMessage = "";
        public bool dados = false;
        private PessoaDAO dao;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                pessoa.Cpf = Request.Form["cpf"];
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
                    errorMessage = "Digite um CPF válido.";
                    return;
                }
            }
            catch (ArgumentOutOfRangeException erro)
            {
                this.errorMessage = "O CPF precisa ser preenchido corretamente.";
                return;
            }
            catch (FormatException erro)
            {
                errorMessage = "Digite números válidos.";
                return;
            }
            catch (Exception erro)
            {
                errorMessage = erro.Message;
                return;
            }

            try
            {
                dao = new PessoaDAO();
                pessoa = dao.BuscaPessoaCPF(Request.Form["cpf"]);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }

            dados = true;
        }
    }
}
