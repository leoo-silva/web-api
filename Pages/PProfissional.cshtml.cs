using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.DAO;
using web.Entities.Pessoa.Model;


namespace web.Pages
{
    public class PProfissionalModel : PageModel
    {
        private PessoaDAO dao;
        public Pessoa pessoa = new Pessoa();
        public string errorMessage = "";
        public bool dados = false;
        public List<Pessoa> list;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                pessoa.Profissao = Request.Form["profissao"];
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }

            try
            {
                dao = new PessoaDAO();
                list = dao.BuscandoProfissao(pessoa.Profissao);

                if (list.Count == 0)
                {
                    this.errorMessage = "Profissão não encontrada. Tente novamente!";
                    return;
                }
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }

            this.dados = true;
        }
    }
}
