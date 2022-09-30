using System;
using System.Collections.Generic;
using ALfredoMochileiro.Models;

namespace ALfredoMochileiro
{
  class Program
  {
    static void Main(string[] args)
    {
      //Variaveis 
      const float CapacidadeMaxima = 15;
      const int NumMochilas = 20;
      float aptidaoMaxima = 0;
      //   Console.WriteLine("Hello Worlds");
      List<Item> itensDisponiveis = new List<Item>();
      List<Mochila> populacao = new List<Mochila>();

      //Lista de itens
      itensDisponiveis.Add(new Item(5, 2, "Arvore"));
      itensDisponiveis.Add(new Item(2, 2, "Capacete"));
      itensDisponiveis.Add(new Item(3, 8, "Celular"));
      itensDisponiveis.Add(new Item(4, 10, "Computador"));
      itensDisponiveis.Add(new Item(4, 4, "Macaco"));
      itensDisponiveis.Add(new Item(3, 6, "Moletom"));
      itensDisponiveis.Add(new Item(5, 1, "Pedra"));
      itensDisponiveis.Add(new Item(1, 6, "Relogio"));
      itensDisponiveis.Add(new Item(2, 7, "ventilador"));
      itensDisponiveis.Add(new Item(3, 5, "violao"));
      itensDisponiveis.Sort((a, b) => a.Nome.CompareTo(b.Nome));

      //Interação com o usuário
      Console.WriteLine("Querido usuário, quantas gerações você deseja que existam nessa linda pesquisa?");
      int geracoes = Convert.ToInt32(Console.ReadLine());
      int geracoesContabilizadas = (int)Math.Ceiling(geracoes / 10.0);
      Console.WriteLine("Querido usuário, e a taxa de mutação? Entre 0 e 100");
      int taxaMutacao = Convert.ToInt32(Console.ReadLine());

      //Inicializa a população      
      for (int i = 0; i < NumMochilas; i++)
      {
        Mochila aux = new Mochila(CapacidadeMaxima, new List<Item>(itensDisponiveis));
        populacao.Add(aux);
        aptidaoMaxima += aux.Aptidations;
      }
      int pontoParada = 0;
      //Exibir varias gerações
      for (int contador = 0; contador < geracoes; contador++)
      {
        //Verificação de igualdade entre a geração 0 e a ultima geração
        if (populacao[0].Aptidations == populacao[populacao.Count - 1].Aptidations)
        {
          pontoParada++;
        }
        else
        {
          pontoParada = 0;
        }
        //Se os elementos das gerações 0 e a ultima forem iguais depois de 100x
        if (pontoParada >= 100)
        {
          Console.WriteLine("Geração " + (contador + 1) + " foi a geração que se encontra todas as mochilas com a mesma aptidão");
          break;
        }

        //Sortear de acordo com aptidão 
        populacao.Sort((a, b) => b.Aptidations.CompareTo(a.Aptidations));
        Console.WriteLine((contador + 1) + " - " + geracoes);
        if (contador % geracoesContabilizadas == 0)
        {
          Console.WriteLine("================================ GERAÇÃO: " + (contador + 1) + "================================");
          for (int i = 0; i < NumMochilas; i++)
          {
            Console.WriteLine("Mochila " + (i + 1) + "\n" + populacao[i]);
          }
        }

        List<(Mochila moc, int inicio, int fim)> ranges = new List<(Mochila, int, int)>();
        ranges.AddRange(RoletaRussa(populacao));
        Random rand = new Random();
        int rangeSorteado = rand.Next(1000000);
        while (rangeSorteado > ranges[ranges.Count - 1].fim)
        {
          Console.WriteLine("[AZAR DO CARAIO]");
          rangeSorteado = rand.Next(1000000);
        }
        var moc1 = ranges.Find(a => a.inicio <= rangeSorteado && a.fim >= rangeSorteado);
        ranges.Remove(moc1);
        ranges = new List<(Mochila, int, int)>();
        ranges.AddRange(RoletaRussa(populacao, moc1.moc));
        rangeSorteado = rand.Next(1000000);
        while (rangeSorteado > ranges[ranges.Count - 1].fim)
        {
          Console.WriteLine("AZAR DO CARAIO");
          rangeSorteado = rand.Next(1000000);
        }
        var moc2 = ranges.Find(a => a.inicio <= rangeSorteado && a.fim >= rangeSorteado);

        //Console.WriteLine("GANHADORES:");
        //Console.WriteLine(moc1.moc);
        //Console.WriteLine(moc2.moc);
        Mochila f1, f2;
        (f1, f2) = Cruzamento(moc1.moc, moc2.moc, new List<Item>(itensDisponiveis), taxaMutacao);
        populacao.Add(f1);
        populacao.Add(f2);
        populacao.Sort((a, b) => b.Aptidations.CompareTo(a.Aptidations));

        populacao.RemoveAt(populacao.Count() - 1);
        populacao.RemoveAt(populacao.Count() - 1);
      }

      //Exibição depois do cruzamento
      Console.WriteLine("\n=========================================================GERACAO FINAL=========================================================");
      for (int i = 0; i < NumMochilas; i++)
      {
        Console.WriteLine("Mochila " + (i + 1) + "\n" + populacao[i]);
      }
    }

    //Calculo da aptidão maxima
    public static float getAptidaoMaxima(List<Mochila> mochilas)
    {
      float aptidaoMax = 0;
      foreach (Mochila mochila in mochilas)
      {
        aptidaoMax += mochila.Aptidations;
      }
      return aptidaoMax;
    }

    public static float getAptidaoMaxima(List<Mochila> mochilas, Mochila moc)
    {
      float aptidaoMax = 0;
      foreach (Mochila mochila in mochilas)
      {
        if (!mochila.Equals(moc))
          aptidaoMax += mochila.Aptidations;
      }
      return aptidaoMax;
    }

    public static int calculaRange(float aptidao, float aptidaoMaxima)
    {
      return (int)((aptidao / aptidaoMaxima) * 1000000);
    }

    public static List<(Mochila, int, int)> RoletaRussa(List<Mochila> populacao, Mochila? mochila = null)
    {
      float aptidaoMaxima = 0;
      if (mochila != null)
      {
        aptidaoMaxima = getAptidaoMaxima(populacao, mochila);
      }
      else
      {
        aptidaoMaxima = getAptidaoMaxima(populacao);
      }

      List<(Mochila moc, int inicio, int fim)> ranges = new List<(Mochila, int, int)>();
      for (int j = 0, aux = 0; j < populacao.Count; j++)
      {
        if (!populacao[j].Equals(mochila))
        {
          int range = calculaRange(populacao[j].Aptidations, aptidaoMaxima);
          if (j == 0 || aux == 0)
            ranges.Add((populacao[j], 0, range));
          else
            ranges.Add((populacao[j], ranges[aux - 1].fim + 1, ranges[aux - 1].fim + range));

          //Console.WriteLine("[ " + aux + " ] - " + ranges[aux].inicio + " | | " + ranges[aux].fim);
          aux++;
        }
      }
      return ranges;
    }

    //Cruzamento pai e mãe (mochilas)
    public static (Mochila, Mochila) Cruzamento(Mochila m1, Mochila m2, List<Item> itensDisponiveis, int taxaMutacao)
    {
      Random rand = new Random();
      int divisor = rand.Next(itensDisponiveis.Count());

      //Criação do Filho 1
      List<Item> listaFilho1 = new List<Item>();
      for (int i = 0; i < divisor; i++)
      {
        if (m1.GetItems().Contains(itensDisponiveis[i]))
          listaFilho1.Add(itensDisponiveis[i]);
      }
      for (int i = itensDisponiveis.Count() - 1; i >= divisor; i--)
      {

        if (m2.GetItems().Contains(itensDisponiveis[i]))
          listaFilho1.Add(itensDisponiveis[i]);
      }
      int num = rand.Next(100) + 1;
      bool temMutacao = taxaMutacao >= num;

      if (temMutacao)
      {
        while (true)
        {
          num = rand.Next(itensDisponiveis.Count());
          if (!listaFilho1.Contains(itensDisponiveis[num]))
          {
            listaFilho1.Add(itensDisponiveis[num]);
            if (num % 2 == 0)
            {
              num = rand.Next(listaFilho1.Count());
              listaFilho1.RemoveAt(num);
            }
            break;
          }
        }
        Console.WriteLine("Teve mutacao no filho 1");
      }
      Mochila filho1 = new Mochila(15, listaFilho1, true);

      //Criação do Filho 2
      List<Item> listaFilho2 = new List<Item>();
      //Intens da mae
      for (int i = 0; i < divisor; i++)
      {
        if (m2.GetItems().Contains(itensDisponiveis[i]))
          listaFilho2.Add(itensDisponiveis[i]);
      }
      //Itens do pai
      for (int i = itensDisponiveis.Count() - 1; i >= divisor; i--)
      {
        if (m1.GetItems().Contains(itensDisponiveis[i]))
          listaFilho2.Add(itensDisponiveis[i]);
      }

      //Verifica se tem Mutação
      num = rand.Next(100) + 1;
      temMutacao = taxaMutacao >= num;
      if (temMutacao)
      {
        while (true)
        {
          num = rand.Next(itensDisponiveis.Count());
          if (!listaFilho2.Contains(itensDisponiveis[num]))
          {
            listaFilho2.Add(itensDisponiveis[num]);
            if (num % 2 == 0)
            {
              num = rand.Next(listaFilho2.Count());
              listaFilho2.RemoveAt(num);
            }
            break;
          }
        }
        Console.WriteLine("Teve mutacao no filho 2");
      }

      Mochila filho2 = new Mochila(15, listaFilho2, true);

      //Exibição do filho 1
      //foreach (var item in filho1.GetItems())
      // {
      //  Console.WriteLine("Itens do filho 1: " + item);
      // };
      Console.WriteLine("Apitidão do filho 1: " + filho1.Aptidations);

      //Exibição do filho 2
      //   foreach (var item in filho2.GetItems())
      //   {
      //     Console.WriteLine("Itens do filho 2: " + item);
      //   };
      Console.WriteLine("Apitidão do filho 2: " + filho2.Aptidations);
      return (filho1, filho2);
    }
  }
}