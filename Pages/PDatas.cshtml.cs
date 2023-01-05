using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using web.Entities.Pessoa.Model;
using web.Entities.Pessoa.DAO;
using web.Entities.PException;


namespace web.Pages
{
    public class PDatasModel : PageModel
    {
        private PessoaBetween pessoaBet;
        private PessoaDAO dao;
        public bool dados = false;
        public PessoaInfo pessoa = new PessoaInfo();
        public string errorMessage = "";
        public List<PessoaInfo> list;

        public void OnGet()
        {

        }

        public void OnPost()
        {
            pessoaBet = new PessoaBetween();
            try
            {
                pessoaBet.SetDe(DateTime.Parse(Request.Form["de"]));
                pessoaBet.SetAte(DateTime.Parse(Request.Form["ate"]));
            }
            catch (Exception erro)
            {
                this.errorMessage = "Digite as duas datas corretamente.";
                return;
            }
            
            try
            {
                dao = new PessoaDAO();
                this.list = dao.SelectBetween(this.pessoaBet);

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
