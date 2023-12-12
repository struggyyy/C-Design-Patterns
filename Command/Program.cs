using System;

public interface IPolecenie
{
    void wykonaj();
}

public class KomendaWlacz : IPolecenie
{
    Lampa lampa;
    public KomendaWlacz(Lampa lampa)
    {
        this.lampa = lampa;
    }
    public void wykonaj()
    {
        lampa.wlacz();
    }
}

public class KomendaWylacz : IPolecenie
{
    Lampa lampa;
    public KomendaWylacz(Lampa lampa)
    {
        this.lampa = lampa;
    }
    public void wykonaj()
    {
        lampa.wylacz();
    }
}

public class Lampa
{
    private bool stan = false;

    public void wlacz()
    {
        stan = true;
    }

    public void wylacz()
    {
        stan = false;
    }

    public bool sprawdz()
    {
        return stan;
    }
}

public class Pilot
{
    private IPolecenie polecenie;

    public void ustawPolecenie(IPolecenie polecenie)
    {
        this.polecenie = polecenie;
    }

    public void wcisnijGuzik()
    {
        polecenie.wykonaj();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Lampa lampa = new Lampa();

        IPolecenie wlacz = new KomendaWlacz(lampa);
        IPolecenie wylacz = new KomendaWylacz(lampa);

        Console.WriteLine(lampa.sprawdz() ? "Lampa włączona" : "Lampa wyłączona");

        Pilot pilot = new Pilot();

        pilot.ustawPolecenie(wlacz);
        pilot.wcisnijGuzik();
        Console.WriteLine(lampa.sprawdz() ? "Lampa włączona" : "Lampa wyłączona");

        pilot.ustawPolecenie(wylacz);
        pilot.wcisnijGuzik();
        Console.WriteLine(lampa.sprawdz() ? "Lampa włączona" : "Lampa wyłączona");
    }
}
