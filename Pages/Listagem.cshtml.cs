using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;


namespace web.Pages
{
    public class ListagemModel : PageModel
    {
        private PessoaDAO dao;
        public List<Pessoa> list;

        public void OnGet()
        {
            try
            {
                dao = new PessoaDAO();
                list = dao.BuscaDados();
            }
            catch (Exception erro)
            {
                throw new Exception("Error: " + erro.Message);
            }
        }
    }
}
