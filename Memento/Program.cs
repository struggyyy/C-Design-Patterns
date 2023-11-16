using System;
using System.Collections.Generic;

namespace Memento
{
    public interface IMovie
    {
        IMemento Save();
        void Restore(IMemento memento);
    }

    class BackToTheFuture : IMovie
    {
        private int Year;

        public BackToTheFuture(int year)
        {
            // Początkowa wartość
            Console.WriteLine("Początkowy rok: " + year);
            this.Year = year;
        }

        public void SetYear(int year)
        {
            // Ustawia pole na właściwą wartość
            Console.WriteLine("Rok zmieniony na: " + year);
            this.Year = year;
        }

        public IMemento Save()
        {
            return new Memento(this.Year);
        }

        public void Restore(IMemento memento)
        {
            // Przywraca wartość pola
            this.Year = ((Memento)memento).GetYear();
            Console.WriteLine("Przywrócony rok: " + this.Year);
        }
    }

    public interface IMemento
    {
        // Interfejs dla pamiątki
    }

    class Memento : IMemento
    {
        private int Year;

        public Memento(int year)
        {
            // Konstruktor pamiątki
            this.Year = year;
        }

        public int GetYear()
        {
            // Zwraca rok
            return this.Year;
        }
    }

    class Caretaker
    {
        private List<IMemento> Mementos = new List<IMemento>();
        private IMovie movie;

        public Caretaker(IMovie movie)
        {
            this.movie = movie;
        }

        public void Save()
        {
            // Dodaje pamiętkę do listy pamiątek
            IMemento memento = movie.Save();
            Mementos.Add(memento);
            Console.WriteLine("Zapisano pamiątkę z roku: " + ((Memento)memento).GetYear());
        }

        public void Undo()
        {
            // Print, jeśli nie ma pamiątek do przywrócenia
            if (Mementos.Count == 0)
            {
                Console.WriteLine("Nie można cofnąć - brak zapisanych danych");
                return;
            }

            var memento = this.Mementos[this.Mementos.Count - 1];

            // Wyciągniętą pamiątkę trzeba skasować i przywrócić
            this.Mementos.Remove(memento);
            movie.Restore(memento);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BackToTheFuture favoriteMovie = new BackToTheFuture(1985);
            Caretaker caretaker = new Caretaker(favoriteMovie);

            caretaker.Undo(); // Test ;)

            Console.WriteLine();

            Console.WriteLine("Część I:");
            favoriteMovie.SetYear(1955);
            caretaker.Save();
            favoriteMovie.SetYear(1985);

            Console.WriteLine();

            Console.WriteLine("Część II:");
            favoriteMovie.SetYear(2015);
            favoriteMovie.SetYear(1985);
            caretaker.Undo();
            favoriteMovie.SetYear(1985);
            caretaker.Save();

            Console.WriteLine();

            Console.WriteLine("Część III:");
            favoriteMovie.SetYear(1885);
            caretaker.Undo();
        }
    }
}
