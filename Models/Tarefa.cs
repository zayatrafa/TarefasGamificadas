using System;
using System.Text.Json.Serialization;

namespace TarefasGamificadas.Models
{
    public class Tarefa
    {
        public string Nome { get; set; }
        public int Pontos { get; set; }
        public int RecorrenciaDias { get; set; }
        public DateTime UltimaExecucao { get; set; }
        public Guid ResponsavelId { get; set; } 

        [JsonIgnore]  // Ignorar na serialização
        public Usuario Responsavel { get; set; }

        public Tarefa() { }

        public Tarefa(string nome, Usuario responsavel, int pontos, int recorrenciaDias)
        {
            Nome = nome;
            Responsavel = responsavel;
            ResponsavelId = responsavel.Id;
            Pontos = pontos;
            RecorrenciaDias = recorrenciaDias;
            UltimaExecucao = DateTime.MinValue;
        }

        public void MarcarComoConcluida()
        {
            UltimaExecucao = DateTime.Now;
            Responsavel.AdicionarPontos(Pontos);
        }

        public bool EstaAtrasada()
        {
            return (DateTime.Now - UltimaExecucao).TotalDays > RecorrenciaDias;
        }
    }
}
