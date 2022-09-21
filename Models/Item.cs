using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ALfredoMochileiro.Models
{
    public class Item
    {
        public float Peso { get; set; }
        public float Preco { get; set; }
        public string Nome { get; set; }
        public Item(float peso, float preco, string nome)
        {
            this.Peso = peso;
            this.Preco = preco;
            this.Nome = nome;
        }

        public override string ToString()
        {
            return this.Nome + " - Peso: " + this.Peso + " - preco: " + this.Preco;
        }
    }
}