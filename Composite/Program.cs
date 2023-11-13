using System;
using System.Collections.Generic;

abstract class Element
{
    public abstract void Renderuj();
}

class Liść : Element
{
    private string nazwa;

    public Liść(string nazwa)
    {
        this.nazwa = nazwa;
    }

    public override void Renderuj()
    {
        Console.WriteLine($"{nazwa} renderowanie...");
    }
}

class Węzeł : Element
{
    private string nazwa;
    private List<Element> dzieci = new List<Element>();

    public Węzeł(string nazwa)
    {
        this.nazwa = nazwa;
    }

    public void DodajElement(Element element)
    {
        dzieci.Add(element);
    }

    public override void Renderuj()
    {
        Console.WriteLine($"{nazwa} rozpoczęcie renderowania");

        foreach (var dziecko in dzieci)
        {
            dziecko.Renderuj();
        }

        Console.WriteLine($"{nazwa} zakończenie renderowania");
    }
}

class MainClass
{
    public static void Main(string[] args)
    {
        Węzeł korzen = new Węzeł("Korzeń");
        korzen.DodajElement(new Liść("Liść 1.1"));

        Węzeł węzeł2 = new Węzeł("Węzeł 2");
        węzeł2.DodajElement(new Liść("Liść 2.1"));
        węzeł2.DodajElement(new Liść("Liść 2.2"));
        węzeł2.DodajElement(new Liść("Liść 2.3"));

        Węzeł węzeł3 = new Węzeł("Węzeł 3");
        węzeł3.DodajElement(new Liść("Liść 3.1"));
        węzeł3.DodajElement(new Liść("Liść 3.2"));

        Węzeł węzeł3_3 = new Węzeł("Węzeł 3.3");
        węzeł3_3.DodajElement(new Liść("Liść 3.3.1"));

        węzeł3.DodajElement(węzeł3_3);

        korzen.DodajElement(węzeł2);
        korzen.DodajElement(węzeł3);

        korzen.Renderuj();
    }
}