using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALfredoMochileiro.Models
{
    public class Mochila
    {
        private List<Item> Itens = new List<Item>();
        public float Aptidations = 0;
        private float CapacidadeMaxima;
        private float Capacidade { get; set; }
        private float PrecoTotal = 0;

        public Mochila(float capacidadeMaxima, List<Item> itensDisponiveis)
        {
            this.CapacidadeMaxima = capacidadeMaxima;
            var rand = new Random();
            int numItens = rand.Next(itensDisponiveis.Count());
            if (numItens == 0)
            {
                numItens++;
            }
            for (int i = 0; i < numItens; i++)
            {
                int id = rand.Next(itensDisponiveis.Count());
                this.Itens.Add(itensDisponiveis[id]);
                this.Capacidade += itensDisponiveis[id].Peso;
                this.PrecoTotal += itensDisponiveis[id].Preco;
                itensDisponiveis.Remove(itensDisponiveis[id]);
            }
        }

        public override string ToString()
        {
            string retorno = "";
            foreach (Item item in Itens)
            {
                retorno += "\t" + item.ToString() + "\n";
            }
            retorno += "Capacidade: " + this.Capacidade + "\n";
            retorno += "Preço Total: " + this.PrecoTotal + "\n";
            return retorno;
        }
    }
}