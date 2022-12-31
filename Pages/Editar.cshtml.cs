using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;
using web.Entities.PException;


namespace web.Pages
{
    public class EditarModel : PageModel
    {
        private PessoaDAO dao = new PessoaDAO();
        public PessoaInfo pessoa = new PessoaInfo();
        public string errorMessage = "";
        public string successMessage = "";

        public void OnGet()
        {
            string cpf = Request.Query["cpf"];

            try
            {
                pessoa = dao.SelectCPF(cpf);
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
                pessoa.SetCpf(Request.Query["cpf"]);
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

            // salvando dados caso não haja erro
            try
            {
                dao.Update(pessoa);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
            }

            Response.Redirect("/Listagem");
        }
    }
}
