namespace MethodMonteCarlo
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    class Program
    {
        static void Main()
        {
            int rows = 100;
            int cols = 100;

            long[,] tabelaAleatoria = CriarTabelaAleatoria(rows, cols);

            Console.Write("Digite a quantidade de dígitos que deseja coletar (1-10): ");
            int quantidadeDigitos = int.Parse(Console.ReadLine());

            Console.Write("Digite a posição que deseja coletar: (menor que" + (10 - quantidadeDigitos) + ")");
            int posicaoColeta = int.Parse(Console.ReadLine());

            int[] quantidadesColetas = { 40, 400, 4000, 40000, 400000, 4000000 };

            double valorTotal;

            foreach (int quantidadeColetas in quantidadesColetas)
            {
            valorTotal = 0;
                List<long> valoresColetados = ColetarValores(tabelaAleatoria, quantidadeColetas, posicaoColeta, quantidadeDigitos);

                Console.WriteLine($"Quantidade de Coletas: {quantidadeColetas}, Valores Coletados: {string.Join(", ", valoresColetados)}");
                Console.WriteLine($"--------------------------------------------------------------------------------------------------------");
                valoresColetados.ForEach(valor =>
                {
                    Console.WriteLine($"Valores somados: {SomarAlgarismos(valor)}");
                    Console.WriteLine($"--------------------------------------------------------------------------------------------------------");
                    valorTotal += SomarAlgarismos(valor);
                });

                Console.WriteLine($"Soma total: {valorTotal} -  Média geral: {valorTotal / valoresColetados.Count}");
                Console.WriteLine($"--------------------------------------------------------------------------------------------------------");

            }
        }

        static long[,] CriarTabelaAleatoria(int rows, int cols)
        {
            long[,] tabelaAleatoria = new long[rows, cols];
            Random random = new Random();

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    tabelaAleatoria[i, j] = GerarNumeroAleatorio(10, random); // Gera números de 10 dígitos
                }
            }

            return tabelaAleatoria;
        }

        static long GerarNumeroAleatorio(int digitos, Random random)
        {
            long min = (long)Math.Pow(10, digitos - 1);
            long max = (long)Math.Pow(10, digitos) - 1;
            return random.Next((int)min, (int)max + 1);
        }

        static List<long> ColetarValores(long[,] tabela, int quantidade, int posicaoColeta, int quantidadeDigitos)
        {
            List<long> valoresColetados = new List<long>();

            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {

                    long novoValor = PegarNumeroNaPosicao(tabela[i, j], posicaoColeta, quantidadeDigitos);
                    valoresColetados.Add(novoValor);
                    if (((i*100) + j) >= quantidade)
                    {
                        return valoresColetados;
                    }
                }
            }

            return valoresColetados;
        }

        private static long PegarNumeroNaPosicao(long numero, int posicaoColeta, int quantidadeDigitos)
        {
            if (posicaoColeta < 0 || posicaoColeta + quantidadeDigitos > 10)
            {
                throw new ArgumentException("A posição de coleta e/ou a quantidade de dígitos não são válidos.");
            }

            long fatorDeslocamento = (long)Math.Pow(10, 10 - quantidadeDigitos - posicaoColeta);

            long numeroColetado = (numero / fatorDeslocamento) % (long)Math.Pow(10, quantidadeDigitos);

            return numeroColetado;
        }

        static long SomarAlgarismos(long numero)
        {
            long soma = 0;
            while (numero != 0)
            {
                soma += numero % 10;
                numero /= 10;
            }
            return soma;
        }
    }
}
