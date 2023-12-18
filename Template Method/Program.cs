using System;

abstract class ZamowienieTemplatka
{
    public void przetwarzajZamowienie(bool czyGratis)
    {
        doKoszyk();
        doPlatnosc();
        if (czyGratis)
        {
            dodanieGratisu();
        }
        doDostawa();
    }

    public void dodanieGratisu()
    {
        Console.WriteLine("Dodano gratis...");
    }

    protected abstract void doKoszyk();
    protected abstract void doPlatnosc();
    protected abstract void doDostawa();
}

class ZamowienieOnline : ZamowienieTemplatka
{
    protected override void doKoszyk()
    {
        Console.WriteLine("Kompletowanie zamówienia...");
        Console.WriteLine("Ustawiono parametry wysyłki...");
    }

    protected override void doPlatnosc()
    {
        Console.WriteLine("Płatność...");
    }

    protected override void doDostawa()
    {
        Console.WriteLine("Wysyłka...");
    }
}

class ZamowienieStacjonarne : ZamowienieTemplatka
{
    protected override void doKoszyk()
    {
        Console.WriteLine("Wybranie produktów...");
    }

    protected override void doPlatnosc()
    {
        Console.WriteLine("Płatność w kasie (karta/gotówka)...");
    }

    protected override void doDostawa()
    {
        Console.WriteLine("Wydanie produktów (odbiór osobisty)...");
    }
}

class Program
{
    public static void Main(String[] args)
    {
        ZamowienieTemplatka zamowienieOnline = new ZamowienieOnline();
        zamowienieOnline.przetwarzajZamowienie(true);

        Console.WriteLine();

        ZamowienieTemplatka zamowienieStacjonarne = new ZamowienieStacjonarne();
        zamowienieStacjonarne.przetwarzajZamowienie(false);
    }
}
