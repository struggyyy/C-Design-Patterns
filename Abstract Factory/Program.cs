using System;
using System.Text;

interface ILetters
{
    string ShowAlfa();
}

interface INums
{
    string ShowNum();
}

class AlphabetFactory
{
    private SystemFactory systemFactory;
    public ILetters letters;
    public INums numbers;

    public AlphabetFactory(SystemFactory systemFactory)
    {
        this.systemFactory = systemFactory;
    }

    public void Generate()
    {
        letters = systemFactory.CreateAlfa();
        numbers = systemFactory.CreateNum();
    }
}

abstract class SystemFactory
{
    public abstract ILetters CreateAlfa();
    public abstract INums CreateNum();
}

class LacinkaFactory : SystemFactory
{
    public override ILetters CreateAlfa()
    {
        return new LacinkaLetters();
    }

    public override INums CreateNum()
    {
        return new LacinkaNumbers();
    }
}

class CyrylicaFactory : SystemFactory
{
    public override ILetters CreateAlfa()
    {
        return new CyrylicaLetters();
    }

    public override INums CreateNum()
    {
        return new CyrylicaNumbers();
    }
}

class GrekaFactory : SystemFactory
{
    public override ILetters CreateAlfa()
    {
        return new GrekaLetters();
    }

    public override INums CreateNum()
    {
        return new GrekaNumbers();
    }
}

class LacinkaLetters : ILetters
{
    public string ShowAlfa()
    {
        return "abcde";
    }
}

class CyrylicaLetters : ILetters
{
    public string ShowAlfa()
    {
        return "абвгд";
    }
}

class GrekaLetters : ILetters
{
    public string ShowAlfa()
    {
        return "αβγδε";
    }
}

class LacinkaNumbers : INums
{
    public string ShowNum()
    {
        return "I II III";
    }
}

class CyrylicaNumbers : INums
{
    public string ShowNum()
    {
        return "1 2 3";
    }
}

class GrekaNumbers : INums
{
    public string ShowNum()
    {
        return "αʹ βʹ γʹ";
    }
}

public class Application
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        AlphabetFactory alphabetLacinka = new AlphabetFactory(new LacinkaFactory());
        alphabetLacinka.Generate();

        AlphabetFactory alphabetCyrylica = new AlphabetFactory(new CyrylicaFactory());
        alphabetCyrylica.Generate();

        AlphabetFactory alphabetGreka = new AlphabetFactory(new GrekaFactory());
        alphabetGreka.Generate();

        //Console.WriteLine("Alfabet i cyfry w systemie łacińskim: ");
        Console.WriteLine(alphabetLacinka.letters.ShowAlfa() + " " + alphabetLacinka.numbers.ShowNum());

        //Console.WriteLine("Alfabet i cyfry w systemie cyrylickim: ");
        Console.WriteLine(alphabetCyrylica.letters.ShowAlfa() + " " + alphabetCyrylica.numbers.ShowNum());

        //Console.WriteLine("Alfabet i cyfry w systemie greckim: ");
        Console.WriteLine(alphabetGreka.letters.ShowAlfa() + " " + alphabetGreka.numbers.ShowNum());
    }
}
