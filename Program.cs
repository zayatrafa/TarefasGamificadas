using System;
using System.Collections.Generic;
using TarefasGamificadas.Models;
using TarefasGamificadas.Utils;

namespace TarefasGamificadas
{
    internal class Program
    {
        // Lista de usuários do sistema
        static List<Usuario> usuarios = new List<Usuario>();

        // Usuário atualmente logado
        static Usuario? usuarioLogado = null;

        static void Main(string[] args)
        {
            int opcao;
            do
            {
                Console.Clear();
                Console.WriteLine("== MENU TAREFAS GAMIFICADAS ==");
                Console.WriteLine("1 - Criar novo usuário");
                Console.WriteLine("2 - Logar com usuário existente");
                Console.WriteLine("3 - Adicionar nova tarefa");
                Console.WriteLine("4 - Concluir uma tarefa");
                Console.WriteLine("5 - Verificar tarefas atrasadas");
                Console.WriteLine("6 - Ver meus pontos");
                Console.WriteLine("0 - Sair");

                // Leitura da opção
                opcao = EntradaUtils.LerInteiro("Escolha uma opção: ", 0, 6);

                Console.Clear();

                // Execução de cada ação
                switch (opcao)
                {
                    case 1:
                        CriarUsuario();
                        break;
                    case 2:
                        LogarUsuario();
                        break;
                    case 3:
                        AdicionarTarefa();
                        break;
                    case 4:
                        ConcluirTarefa();
                        break;
                    case 5:
                        VerificarAtrasadas();
                        break;
                    case 6:
                        VerPontos();
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                Console.WriteLine("\nPressione ENTER para continuar...");
                Console.ReadLine();

            } while (opcao != 0);
        }

        static void CriarUsuario()
        {
            string nome;
            nome = EntradaUtils.LerTexto("Digite o nome do novo usuário: ");

            usuarios.Add(new Usuario(nome));
            Console.WriteLine("Usuário criado com sucesso!");
        }

        static void LogarUsuario()
        {
            string nome = EntradaUtils.LerTexto("Digite o nome do usuário: ");
            usuarioLogado = usuarios.Find(u => u.Nome == nome);

            if (usuarioLogado != null)
                Console.WriteLine($"Logado como {usuarioLogado.Nome}");
            else
                Console.WriteLine("Usuário não encontrado.");
        }

        static void AdicionarTarefa()
        {
            if (usuarioLogado == null)
            {
                Console.WriteLine("Você precisa estar logado.");
                return;
            }

            string nomeTarefa = EntradaUtils.LerTexto("Nome da tarefa: ");

            int pontos = EntradaUtils.LerInteiro("Quantos pontos vale? ");

            int recorrencia = EntradaUtils.LerInteiro("Recorrência (em dias): ");

            try
            {
                var tarefa = new Tarefa(nomeTarefa, usuarioLogado, pontos, recorrencia);
                usuarioLogado.Tarefas.Add(tarefa);
                Console.WriteLine("Tarefa adicionada com sucesso!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocorreu um erro ao adicionar a tarefa: {ex.Message}");
            }
        }


        static void ConcluirTarefa()
        {
            if (usuarioLogado == null)
            {
                Console.WriteLine("Você precisa estar logado.");
                return;
            }

            if (usuarioLogado.Tarefas.Count == 0)
            {
                Console.WriteLine("Você não tem tarefas.");
                return;
            }

            Console.WriteLine("Tarefas:");
            for (int i = 0; i < usuarioLogado.Tarefas.Count; i++)
            {
                Console.WriteLine($"{i + 1} - {usuarioLogado.Tarefas[i].Nome}");
            }

            int escolha = EntradaUtils.LerInteiro("Escolha a tarefa que deseja concluir: ");

            usuarioLogado.Tarefas[escolha-1].MarcarComoConcluida();
            Console.WriteLine("Tarefa concluída e pontos adicionados!");

        }

        static void VerificarAtrasadas()
        {
            if (usuarioLogado == null)
            {
                Console.WriteLine("Você precisa estar logado.");
                return;
            }

            var atrasadas = usuarioLogado.Tarefas.FindAll(t => t.EstaAtrasada());

            if (atrasadas.Count == 0)
            {
                Console.WriteLine("Nenhuma tarefa atrasada!");
                return;
            }

            Console.WriteLine("Tarefas atrasadas:");
            foreach (var t in atrasadas)
            {
                Console.WriteLine($"- {t.Nome} (última execução: {t.UltimaExecucao.ToShortDateString()})");
            }
        }

        static void VerPontos()
        {
            if (usuarioLogado == null)
            {
                Console.WriteLine("Você precisa estar logado.");
                return;
            }

            Console.WriteLine($"{usuarioLogado.Nome} tem {usuarioLogado.PontuacaoTotal} pontos.");
        }
    }

}

