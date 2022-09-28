using System;
using System.Collections.Generic;
using ALfredoMochileiro.Models;

namespace ALfredoMochileiro
{
    class Program
    {
        static void Main(string[] args)
        {
            const float CapacidadeMaxima = 15;
            const int NumMochilas = 10;
            float aptidaoMaxima = 0;
            Console.WriteLine("Hello Worlds");
            List<Item> itensUsados = new List<Item>();
            List<Mochila> populacao = new List<Mochila>();
            itensUsados.Add(new Item(3, 5, "violao"));
            itensUsados.Add(new Item(2, 2, "Capacete"));
            itensUsados.Add(new Item(1, 6, "Relogio"));
            itensUsados.Add(new Item(4, 10, "Computador"));
            itensUsados.Add(new Item(3, 8, "Celular"));
            itensUsados.Add(new Item(2, 7, "ventilador"));
            itensUsados.Add(new Item(5, 2, "Arvore"));
            itensUsados.Add(new Item(5, 1, "Pedra"));
            itensUsados.Add(new Item(4, 4, "Macaco"));
            itensUsados.Add(new Item(3, 6, "Moletom"));

            for (int i = 0; i < NumMochilas; i++)
            {
                Mochila aux = new Mochila(CapacidadeMaxima, new List<Item>(itensUsados));
                populacao.Add(aux);
                aptidaoMaxima += aux.Aptidations;
            }

            populacao.Sort((a, b) => b.Aptidations.CompareTo(a.Aptidations));
            for (int i = 0; i < NumMochilas; i++)
            {
                Console.WriteLine("Mochila " + (i + 1) + "\n" + populacao[i]);
            }

            aptidaoMaxima = getAptidaoMaxima(populacao);
            List<(Mochila moc, int inicio, int fim)> ranges = new List<(Mochila, int, int)>();
            for (int j = 0; j < populacao.Count; j++)
            {
                int range = calculaRange(populacao[j].Aptidations, aptidaoMaxima);
                if (j == 0)
                    ranges.Add((populacao[j], 0, range));
                else
                    ranges.Add((populacao[j], ranges[j - 1].fim + 1, ranges[j - 1].fim + range));

                Console.WriteLine("[ " + j + " ] - " + ranges[j].inicio + " | | " + ranges[j].fim);
            }
            Random rand = new Random();
            int rangeSorteado = rand.Next(1000000);
            while (rangeSorteado > ranges[ranges.Count - 1].fim)
            {
                Console.WriteLine("AZAR DO CARAIO");
                rangeSorteado = rand.Next(1000000);
            }
            Mochila moc1 = ranges.Find(a => a.inicio <= rangeSorteado && a.fim >= rangeSorteado).moc;

            rangeSorteado = rand.Next(1000000);
            Mochila moc2 = ranges.Find(a => a.inicio <= rangeSorteado && a.fim >= rangeSorteado).moc;

            Console.WriteLine("GANHADORES:");
            Console.WriteLine(moc1);
            Console.WriteLine(moc2);
        }

        public static float getAptidaoMaxima(List<Mochila> mochilas)
        {
            float aptidaoMax = 0;
            foreach (Mochila mochila in mochilas)
            {
                aptidaoMax += mochila.Aptidations;
            }
            return aptidaoMax;
        }

        public static int calculaRange(float aptidao, float aptidaoMaxima)
        {
            return (int)((aptidao / aptidaoMaxima) * 1000000);
        }


    }
}