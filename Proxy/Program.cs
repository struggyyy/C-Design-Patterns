using System;
using System.Collections.Generic;

namespace Pelnomocnik
{
    public class User
    {
        private bool HasAdminPrivilages;

        public User(bool hasAdminPrivilages)
        {
            HasAdminPrivilages = hasAdminPrivilages;
        }

        public void MakeAdmin()
        {
            HasAdminPrivilages = true;
        }

        public bool IsAdmin()
        {
            return HasAdminPrivilages;
        }
    }

    public interface Information
    {
        void DisplayData();
        void DisplayRestrictedData();
    }

    public class Database : Information
    {
        internal Dictionary<string, double> Map;

        public Database()
        {
            Map = new Dictionary<string, double>
            {
                {"Zyzio MacKwacz", 2500.0},
                {"Scooby Doo", 11.4},
                {"Adam Mackiewicz", 15607.95},
                {"Rick Morty", 429.18}
            };
        }

        public void DisplayData()
        {
            Console.WriteLine("Użytkownicy:");
            foreach (var user in Map.Keys)
            {
                Console.WriteLine(user);
            }
        }

        public void DisplayRestrictedData()
        {
            Console.WriteLine("Nie masz wystarczających uprawnień");
        }
    }

    public class DatabaseGuard : Information
    {
        private Database DB;
        private User user;

        public DatabaseGuard(User u)
        {
            DB = new Database();
            user = u;
        }

        public void DisplayData()
        {
            DB.DisplayData();
        }

        public void DisplayRestrictedData()
        {
            if (user.IsAdmin())
            {
                Console.WriteLine("---------------------------------------------------------");
                foreach (var entry in DB.Map)
                {
                    Console.WriteLine($"{entry.Key} zarabia {entry.Value} zł miesięcznie");
                }
            }
            else
            {
                Console.WriteLine("---------------------------------------------------------");
                DB.DisplayRestrictedData();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var Zbyszek = new User(false);
            var db = new DatabaseGuard(Zbyszek);

            db.DisplayData();

            db.DisplayRestrictedData();

            Zbyszek.MakeAdmin();
            db.DisplayRestrictedData();
        }
    }
}
