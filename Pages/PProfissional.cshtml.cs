using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.DAO;
using web.Entities.Pessoa.Model;
using web.Entities.PException;


namespace web.Pages
{
    public class PProfissionalModel : PageModel
    {
        private PessoaDAO dao;
        public PessoaInfo pessoa = new PessoaInfo();
        public string errorMessage = "";
        public bool dados = false;
        public List<PessoaInfo> list;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            try
            {
                pessoa.SetProfissao(Request.Form["profissao"]);
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }

            try
            {
                dao = new PessoaDAO();
                list = dao.SelectLike(pessoa.GetProfissao());

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
