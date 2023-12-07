using System;
using System.Collections.Generic;

namespace Observer
{
    public enum Genre
    {
        Sport,
        Politics,
        Economy,
        Science
    }

    public interface IObserver
    {
        void Update(ISubject subject);
    }

    public interface ISubject
    {
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }

    public class NewsAgency : ISubject
    {
        private string newsHeadline;
        private Genre state;
        private List<IObserver> observers = new List<IObserver>();

        public void Attach(IObserver observer)
        {
            observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var observer in observers)
            {
                observer.Update(this);
            }
        }

        public void SetNewsHeadline(Genre state, string news)
        {
            this.state = state;
            newsHeadline = news;
            Notify();
        }

        public string NewsHeadline => newsHeadline;
        public Genre State => state;
    }

    class DailyEconomy : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as NewsAgency).State == Genre.Economy)
            {
                Console.WriteLine($"DailyEconomy publikuje artykuł \"{(subject as NewsAgency).NewsHeadline}\"");
            }
        }
    }

    class NewYorkTimes : IObserver
    {
        public void Update(ISubject subject)
        {
            Console.WriteLine($"NewYorkTimes publikuje artykuł \"{(subject as NewsAgency).NewsHeadline}\"");
        }
    }

    class NationalGeographic : IObserver
    {
        public void Update(ISubject subject)
        {
            if ((subject as NewsAgency).State == Genre.Science)
            {
                Console.WriteLine($"NationalGeographic publikuje artykuł \"{(subject as NewsAgency).NewsHeadline}\"");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var newsAgency = new NewsAgency();

            var dailyEconomy = new DailyEconomy();
            var newYork = new NewYorkTimes();
            var nationalGeographic = new NationalGeographic();

            newsAgency.Attach(dailyEconomy);
            newsAgency.Attach(newYork);
            newsAgency.Attach(nationalGeographic);

            newsAgency.SetNewsHeadline(Genre.Economy, "USA is going bancrupt!");
            newsAgency.SetNewsHeadline(Genre.Science, "Life on Alpha Centauri");
            newsAgency.SetNewsHeadline(Genre.Sport, "Adam Małysz is the greatest sportsman in the history of mankind");
            newsAgency.SetNewsHeadline(Genre.Economy, "CD Project RED value has grown by 500% in 2020");
            newsAgency.SetNewsHeadline(Genre.Science, "Kirkendall effect causing airplanes' engine deteriorate");
            newsAgency.SetNewsHeadline(Genre.Politics, "Texas is going bancrupt!");
        }
    }
}
