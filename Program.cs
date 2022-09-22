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
            Console.WriteLine("Hello Worlds");
            List<Item> itensUsados = new List<Item>();
            List<Mochila> populacao = new List<Mochila>();
            itensUsados.Add(new Item(3, 5, "violao"));
            itensUsados.Add(new Item(4, 2, "Capacete"));
            itensUsados.Add(new Item(1, 6, "Relogio"));
            itensUsados.Add(new Item(5, 10, "Computador"));
            itensUsados.Add(new Item(3, 8, "Celular"));
            itensUsados.Add(new Item(2, 7, "ventilador"));
            itensUsados.Add(new Item(9, 2, "Arvore"));
            itensUsados.Add(new Item(7, 1, "Pedra"));

            for (int i = 0; i < 8; i++)
            {
                populacao.Add(new Mochila(CapacidadeMaxima, new List<Item>(itensUsados)));
            }
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine("Mochila " + (i+1) + "\n" + populacao[i]);
            }

        }
    }
}