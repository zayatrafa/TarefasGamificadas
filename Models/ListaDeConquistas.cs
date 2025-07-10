namespace TarefasGamificadas.Models
{
    public static class ListaDeConquistas
    {
        public static List<Conquista> Todas = new List<Conquista>
        {
            new Conquista("Consiga 100 pontos", "Consiga 100 pontos executando tarefas para desbloquear essa conquista", 100),
            new Conquista("Consiga 200 pontos", "Consiga 200 pontos executando tarefas para desbloquear essa conquista", 200),
            new Conquista("Consiga 300 pontos", "Consiga 300 pontos executando tarefas para desbloquear essa conquista", 300),
        };
    }
}
