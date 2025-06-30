using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TarefasGamificadas.Utils
{
    public static class EntradaUtils
    {
        public static int LerInteiro(string mensagem, int valorMinimo = int.MinValue, int valorMaximo = int.MaxValue)
        {
            int valor;
            while (true)
            {
                Console.Write(mensagem);
                string input = Console.ReadLine();

                if (int.TryParse(input, out valor) && valor >= valorMinimo && valor <= valorMaximo)
                {
                    return valor;
                }

                Console.WriteLine($"Valor inválido. Digite um número entre {valorMinimo} e {valorMaximo}.");
            }
        }

        public static string LerTexto(string mensagem)
        {
            while (true)
            {
                Console.Write(mensagem);
                string texto = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    return texto;
                }

                Console.WriteLine("O texto não pode estar vazio.");
            }
        }
    }
}

