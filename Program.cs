using System;
using System.Collections.Generic;
using System.Linq;
using TarefasGamificadas.Models;
using TarefasGamificadas.Utils;
using System.Text.Json;
using System.IO;


namespace TarefasGamificadas
{
    internal class Program
    {
        static readonly string pastaDados = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        static readonly string caminhoArquivo = Path.Combine(pastaDados, "usuarios.json");

        // Lista de usuários do sistema
        static List<Usuario> usuarios = new List<Usuario>();

        // Usuário atualmente logado
        static Usuario? usuarioLogado = null;

        static void Main(string[] args)
        {
            CarregarDados();
            Menu();
        }

        static void Menu()
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

            SalvarDados();
        }

        static void LogarUsuario()
        {
            string nome = EntradaUtils.LerTexto("Digite o nome do usuário: ");
            usuarioLogado = usuarios.FirstOrDefault(u => u.Nome == nome);

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

            SalvarDados();
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

            usuarioLogado.Tarefas[escolha - 1].MarcarComoConcluida();
            Console.WriteLine("Tarefa concluída e pontos adicionados!");

            SalvarDados();
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

        static void SalvarDados()
        {
            try
            {
                
                Directory.CreateDirectory(pastaDados);  // cria a pasta se não existir

                string json = JsonSerializer.Serialize(usuarios, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(caminhoArquivo, json);
                Console.WriteLine($"Arquivo salvo em: {caminhoArquivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao salvar dados: {ex.Message}");
            }
        }

        static void CarregarDados()
        {
            try
            {
                if (File.Exists(caminhoArquivo))
                {
                    
                    string json = File.ReadAllText(caminhoArquivo);
                    usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();

                    Console.WriteLine("Usuários carregados:");
                    foreach (var u in usuarios)
                    {
                        Console.WriteLine($"- {u.Nome} (Id: {u.Id})");
                    }


                    // Reconectar Responsavel nas tarefas
                    foreach (var usuario in usuarios)
                    {
                        foreach (var tarefa in usuario.Tarefas)
                        {
                            tarefa.Responsavel = usuarios.FirstOrDefault(u => u.Id == tarefa.ResponsavelId);
                        }
                    }

                    Console.WriteLine("Dados carregados com sucesso.");
                }
                else
                {
                    usuarios = new List<Usuario>();
                    Console.WriteLine("Nenhum arquivo de dados encontrado. Lista iniciada vazia.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar dados: {ex.Message}");
                usuarios = new List<Usuario>();
            }
        }


    }

}

