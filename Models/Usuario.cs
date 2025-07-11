﻿namespace TarefasGamificadas.Models
{
    public class Usuario
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Nome { get; set; }
        public int PontuacaoTotal { get; set; } = 0;
        public List<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
        public List<ConquistaDesbloqueada> ConquistasDesbloqueadas { get; set; } = new List<ConquistaDesbloqueada>();


        // Construtor sem parâmetros - necessário para desserialização
        public Usuario() { }

        public Usuario(string nome)
        {
            Nome = nome;
        }

        public void AdicionarPontos(int pontos)
        {
            PontuacaoTotal += pontos;
        }
    }
}
