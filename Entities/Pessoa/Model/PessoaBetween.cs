namespace web.Entities.Pessoa.Model
{
    public class PessoaBetween
    {
        private DateTime de;
        private DateTime ate;

        public string GetDeUS()
        {
            return this.de.ToString("yyyy-MM-dd");
        }

        public string GetAteUS()
        {
            return this.ate.ToString("yyyy-MM-dd");
        }

        public string GetDe()
        {
            return this.de.ToString("dd/MM/yyyy");
        }

        public string GetAte()
        {
            return this.ate.ToString("dd/MM/yyyy");
        }

        public void SetDe(DateTime de)
        {
            this.de = de;
        }

        public void SetAte(DateTime ate)
        {
            this.ate = ate;
        }
    }
}
