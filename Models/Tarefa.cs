namespace TarefasGamificadas.Models
{
    internal class Tarefa
    {
        // Nome da tarefa (ex: "Lavar louça")
        public string Nome { get; set; }

        // Usuário responsável por essa tarefa
        public Usuario Responsavel { get; set; }

        // Pontos atribuídos à tarefa
        public int Pontos { get; set; }

        // Recorrência em dias
        public int RecorrenciaDias { get; set; }

        // Última data de execução da tarefa
        public DateTime UltimaExecucao { get; set; }

        // Construtor
        public Tarefa(string nome, Usuario responsavel, int pontos, int recorrenciaDias)
        {
            Nome = nome;
            Responsavel = responsavel;
            Pontos = pontos;
            RecorrenciaDias = recorrenciaDias;
            UltimaExecucao = DateTime.MinValue; // nunca foi feita
        }

        // Marca a tarefa como concluída e dá pontos ao usuário
        public void MarcarComoConcluida()
        {
            UltimaExecucao = DateTime.Now;
            Responsavel.AdicionarPontos(Pontos); // soma pontos ao responsável
        }

        // Verifica se a tarefa está atrasada
        public bool EstaAtrasada()
        {
            return (DateTime.Now - UltimaExecucao).TotalDays > RecorrenciaDias;
        }
    }
}