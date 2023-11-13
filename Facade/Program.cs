using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace WzorzecFasada
{
    interface IUserService
    {
        void CreateUser(string email);
        void DeleteUser(string email);
        int GetNumberOfUsers();
    }

    static class EmailNotification
    {
        public static void SendEmail(string to, string subject)
        {
            Console.WriteLine(subject + " " + to);
        }
    }

    class UserRepository
    {
        internal readonly List<string> users = new List<string>
        {
            "john.doe@gmail.com", "sylvester.stallone@gmail.com"
        };

        public bool IsEmailFree(string email)
        {
            return !users.Contains(email);
        }

        public void AddUser(string email)
        {
            users.Add(email);
        }

        public bool RemoveUser(string email)
        {
            return users.Remove(email);
        }
    }

    static class Validators
    {
        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email,
                @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                RegexOptions.IgnoreCase);
        }
    }

    class UserService : IUserService
    {
        private readonly UserRepository userRepository = new UserRepository();

        public void CreateUser(string email)
        {
            if (!Validators.IsValidEmail(email))
            {
                throw new ArgumentException("Błędny email");
            }

            if (!userRepository.IsEmailFree(email))
            {
                throw new ArgumentException("Email już istnieje");
            }

            userRepository.AddUser(email);
            EmailNotification.SendEmail(email, "Welcome to our service");
        }

        public void DeleteUser(string email)
        {
            if (!userRepository.RemoveUser(email))
            {
                throw new InvalidOperationException("Użytkownik nie istnieje");
            }
            EmailNotification.SendEmail(email, "Goodbye");
        }

        public int GetNumberOfUsers()
        {
            return userRepository.users.Count;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            IUserService userService = new UserService();

            Console.WriteLine("Aktualna liczba adresów: " + userService.GetNumberOfUsers());

            userService.CreateUser("someemail@gmail.com");
            Console.WriteLine("Aktualna liczba adresów: " + userService.GetNumberOfUsers());

            userService.DeleteUser("john.doe@gmail.com");
            Console.WriteLine("Aktualna liczba adresów: " + userService.GetNumberOfUsers());
        }
    }
}
