using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;
using web.Entities.PException;


namespace web.Pages
{
    public class ListagemModel : PageModel
    {
        private PessoaDAO dao;
        public List<PessoaInfo> list;

        public void OnGet()
        {
            try
            {
                dao = new PessoaDAO();
                list = dao.Select();
            }
            catch (Exception erro)
            {
                throw new PessoaException("Error: " + erro.Message);
            }
        }
    }
}
