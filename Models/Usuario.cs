
namespace TarefasGamificadas.Models
{
    internal class Usuario
    {
        public string Nome { get; set; }
        public int PontuacaoTotal { get; set; }

        // Lista de tarefas desse usuário
        public List<Tarefa> Tarefas { get; set; }

        public Usuario(string nome)
        {
            Nome = nome;
            PontuacaoTotal = 0;
            Tarefas = new List<Tarefa>();
        }

        public void AdicionarPontos(int pontos)
        {
            PontuacaoTotal += pontos;
        }
    }
}