using System;

public interface ITelewizor
{
    int Kanal { get; set; }
    void Wlacz();
    void Wylacz();
    void ZmienKanal(int kanal);
}

public class TvLg : ITelewizor
{
    public TvLg()
    {
        this.Kanal = 1;
    }

    public int Kanal { get; set; }

    public void Wlacz()
    {
        Console.WriteLine("Telewizor LG - włączam się. ");
    }

    public void Wylacz()
    {
        Console.WriteLine("Telewizor LG - wyłączam się. ");
    }

    public void ZmienKanal(int kanal)
    {
        this.Kanal = kanal;
        Console.WriteLine("Telewizor LG - zmieniam kanał: " + kanal);
    }
}


public abstract class PilotAbstrakcyjny
{
    protected ITelewizor tv;

    public PilotAbstrakcyjny(ITelewizor tv)
    {
        this.tv = tv;
    }

    public void ZmienKanal(int kanal)
    {
        tv.ZmienKanal(kanal);
    }
}

public class PilotHarmony : PilotAbstrakcyjny
{
    public PilotHarmony(ITelewizor tv) : base(tv) { }

    public void DoWlacz()
    {
        Console.WriteLine("Pilot Harmony - włącz telewizor...");
        tv.Wlacz();
    }

    public void DoZmienKanal(int kanal)
    {
        Console.WriteLine("Pilot Harmony - zmienia kanał...");
        ZmienKanal(kanal);
    }

    public void DoWylacz()
    {
        Console.WriteLine("Pilot Harmony - wyłącz telewizor...");
        tv.Wylacz();
    }
}

public class PilotLG : PilotAbstrakcyjny
{
    public PilotLG(ITelewizor tv) : base(tv) { }

    public void DoWlacz()
    {
        Console.WriteLine("Telewizor LG - włączam się. ");
        tv.Wlacz();
    }

    public void DoZmienKanal(int kanal)
    {
        Console.WriteLine("Pilot LG - zmienia kanał... ");
        ZmienKanal(kanal);
    }

    public void DoWylacz()
    {
        Console.WriteLine("Pilot LG - wyłącza telewizor... ");
        tv.Wylacz();
    }
}

class MainClass
{
    public static void Main(string[] args)
    {
        ITelewizor tv = new TvLg();
        PilotHarmony pilotHarmony = new PilotHarmony(tv);
        PilotLG pilotLG = new PilotLG(tv);

        pilotHarmony.DoWlacz();
        Console.WriteLine("");
        Console.WriteLine("Sprawdź kanał - bieżący kanał: " + tv.Kanal);
        Console.WriteLine("");
        pilotLG.DoZmienKanal(100);
        Console.WriteLine("");
        Console.WriteLine("Sprawdź kanał - bieżący kanał: " + tv.Kanal);
        Console.WriteLine("");
        pilotHarmony.DoWylacz();
    }
}