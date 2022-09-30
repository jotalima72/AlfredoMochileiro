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

    //Construtor para a mochila pai
    public Mochila(float capacidadeMaxima, List<Item> itensDisponiveis)
    {
      this.CapacidadeMaxima = capacidadeMaxima;
      var rand = new Random();
      int numItens = rand.Next(3, itensDisponiveis.Count());

      for (int i = 0; i < numItens; i++)
      {
        int id = rand.Next(itensDisponiveis.Count());
        this.Itens.Add(itensDisponiveis[id]);
        this.Capacidade += itensDisponiveis[id].Peso;
        this.PrecoTotal += itensDisponiveis[id].Preco;
        itensDisponiveis.Remove(itensDisponiveis[id]);
      }
      this.Aptidations = calcularAptidao();
    }

    //Construtor para a mochila filho
    public Mochila(float capacidadeMaxima, List<Item> itens, bool isFilho)
    {
      this.CapacidadeMaxima = capacidadeMaxima;
      for (int i = 0; i < itens.Count(); i++)
      {
        this.Itens.Add(itens[i]);
        this.Capacidade += itens[i].Peso;
        this.PrecoTotal += itens[i].Preco;
      }
      this.Aptidations = calcularAptidao();
    }
    
    public List<Item> GetItems()
    {
        this.Itens.Sort((a,b)=> a.Nome.CompareTo(b.Nome));
        return this.Itens;
      
    }
    private float calcularAptidao()
    {
      float aptidao = this.Capacidade;
      aptidao += this.PrecoTotal * this.PrecoTotal;
      if (this.Capacidade > this.CapacidadeMaxima) aptidao /= (this.Capacidade * this.Capacidade * this.Capacidade);
      return aptidao;
    }

    public override string ToString()
    {
      string retorno = "";
      foreach (Item item in Itens)
      {
        retorno += "|\t" + item.ToString() + "\n";
      }
      retorno += "|Capacidade: " + this.Capacidade + "\n";
      retorno += "|Preço Total: " + this.PrecoTotal + "\n";
      retorno += "|Aptidão: " + this.Aptidations + "\n";
      return retorno;
    }
  }
}