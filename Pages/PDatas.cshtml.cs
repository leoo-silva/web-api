using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;


namespace web.Pages
{
    public class PDatasModel : PageModel
    {
        private PessoaBetween pessoaBet;
        private PessoaDAO dao;
        public bool dados = false;
        public Pessoa pessoa = new Pessoa();
        public string errorMessage = "";
        public List<Pessoa> list;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            pessoaBet = new PessoaBetween();
            try
            {
                pessoaBet.de = DateTime.Parse(Request.Form["de"]);
                pessoaBet.ate = DateTime.Parse(Request.Form["ate"]);
            }
            catch (Exception erro)
            {
                this.errorMessage = "Digite as duas datas corretamente.";
                return;
            }
            
            try
            {
                dao = new PessoaDAO();
                this.list = dao.BuscandoPessoasEntreDatas(this.pessoaBet);

                if (this.list.Count == 0)
                {
                    this.errorMessage = "Não foi possível encontrar pessoas entre as datas informadas. Verifique se as datas foram digitadas corretamente.";
                    return;
                }
            }
            catch (Exception erro)
            {
                this.errorMessage = erro.Message;
                return;
            }
            //finalizar método HTML e correção de bugs


            this.dados = true;
        }
    }
}
