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

        public User(User other)
        {
            HasAdminPrivilages = other.HasAdminPrivilages;
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
            Console.WriteLine("---------------------------------------------------------");
            foreach (var entry in Map)
            {
                Console.WriteLine($"{entry.Key} zarabia {entry.Value} zł miesięcznie");
            }
        }
    }

    public class DatabaseGuard : Information
    {
        private readonly Database DB;
        private readonly User user;

        public DatabaseGuard(User u, Database db)
        {
            DB = db;
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
                DB.DisplayRestrictedData();
            }
            else
            {
                Console.WriteLine("---------------------------------------------------------");
                Console.WriteLine("Nie masz wystarczających uprawnień");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var Zbyszek = new User(false);
            var db = new Database();
            var dbGuard = new DatabaseGuard(Zbyszek, db);

            dbGuard.DisplayData();

            dbGuard.DisplayRestrictedData();

            Zbyszek.MakeAdmin();
            dbGuard.DisplayRestrictedData();
        }
    }
}
