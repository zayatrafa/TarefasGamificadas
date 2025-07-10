namespace TarefasGamificadas.Models
{
    public class Conquista
    {
        public Guid IdConquista = Guid.NewGuid();
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int PontuacaoNecessaria { get; set; }
        public Conquista() { }

        public Conquista(string nome, string descricao, int pontos)
        {
            Nome = nome;
            Descricao = descricao;
            PontuacaoNecessaria = pontos;
        }


    }
}
