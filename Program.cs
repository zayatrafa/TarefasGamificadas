using System;
using System.Collections.Generic;

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
                Console.Write("Escolha uma opção: ");

                // Leitura da opção
                if (!int.TryParse(Console.ReadLine(), out opcao))
                {
                    opcao = -1;
                }

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
            do
            {
                Console.Write("Digite o nome do novo usuário: ");
                nome = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(nome))
                {
                    Console.WriteLine("Nome inválido. Por favor, tente novamente.");
                }
            } while (string.IsNullOrWhiteSpace(nome));

            usuarios.Add(new Usuario(nome));
            Console.WriteLine("Usuário criado com sucesso!");
        }


        static void LogarUsuario()
        {
            Console.Write("Digite o nome do usuário: ");
            string nome = Console.ReadLine();
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

            Console.Write("Nome da tarefa: ");
            string nomeTarefa = Console.ReadLine();

            Console.Write("Quantos pontos vale? ");
            int pontos = int.Parse(Console.ReadLine());

            Console.Write("Recorrência (em dias): ");
            int recorrencia = int.Parse(Console.ReadLine());

            usuarioLogado.Tarefas.Add(new Tarefa(nomeTarefa, usuarioLogado, pontos, recorrencia));

            Console.WriteLine("Tarefa adicionada com sucesso!");
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

            Console.Write("Escolha a tarefa que deseja concluir: ");
            int escolha = int.Parse(Console.ReadLine()) - 1;

            if (escolha >= 0 && escolha < usuarioLogado.Tarefas.Count)
            {
                usuarioLogado.Tarefas[escolha].MarcarComoConcluida();
                Console.WriteLine("Tarefa concluída e pontos adicionados!");
            }
            else
            {
                Console.WriteLine("Opção inválida.");
            }
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
