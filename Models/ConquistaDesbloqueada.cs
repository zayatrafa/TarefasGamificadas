namespace TarefasGamificadas.Models
{
    public class ConquistaDesbloqueada
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public DateTime DataDesbloqueio { get; set; }

        public ConquistaDesbloqueada() { }
        public ConquistaDesbloqueada(string codigo, string nome)
        {
            Codigo = codigo;
            Nome = nome;
            DataDesbloqueio = DateTime.Now;
        }
    }

}
