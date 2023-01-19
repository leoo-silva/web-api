using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;


namespace web.Pages
{
    public class EditarModel : PageModel
    {
        private PessoaDAO dao = new PessoaDAO();
        public Pessoa pessoa = new Pessoa();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string cpf = Request.Query["cpf"];

            try
            {
                pessoa = dao.BuscaPessoaCPF(cpf);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
            }
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
                try
                {
                    pessoa.Peso = float.Parse(Request.Form["peso"]);
                    pessoa.Altura = float.Parse(Request.Form["altura"]);
                }
                catch (FormatException erro)
                {
                    this.errorMessage = "� necess�rio digitar um valor num�rico separados por V�RGULA para os campos de Peso e Altura.";
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
                    errorMessage = "CPF Inv�lido";
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

            if (pessoa.TamanhoCampos())
            {
                this.errorMessage = "Algum campo n�o foi preenchido ou excedeu seu limite de caracteres. Tente Novamente.";
                return;
            }

            // salvando dados caso n�o haja erro
            try
            {
                dao.AlteraDados(pessoa);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
            }

            Response.Redirect("/Listagem");
        }
    }
}
