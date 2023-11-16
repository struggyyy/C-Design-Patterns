using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Threading.Tasks;

namespace Adapter
{

    public class UsersApi
    {
        public async Task<string> GetUsersXmlAsync()
        {
            var apiResponse = "<?xml version=\"1.0\" encoding=\"UTF-8\"?><users><user name=\"John\" surname=\"Doe\"/><user name=\"John\" surname=\"Wayne\"/><user name=\"John\" surname=\"Rambo\"/></users>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(apiResponse);

            return await Task.FromResult(doc.InnerXml);
        }
    }


    public class CsvFileReader
    {
        public string ReadCsvFile(string filePath)
        {
            try
            {
                return File.ReadAllText(filePath, Encoding.UTF8);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd odczytu pliku CSV: {ex.Message}");
                return string.Empty;
            }
        }
    }

    public interface IUserRepository
    {
        List<List<string>> GetUserNames();
    }

    public class UsersApiAdapter : IUserRepository
    {
        private UsersApi _adaptee = null;

        public UsersApiAdapter(UsersApi adaptee)
        {
            _adaptee = adaptee;
        }

        public List<List<string>> GetUserNames()
        {
            string incompatibleApiResponse = this._adaptee
                .GetUsersXmlAsync()
                .GetAwaiter()
                .GetResult();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(incompatibleApiResponse);

            var rootEl = doc.LastChild;

            List<List<string>> users = new List<List<string>>();

            if (rootEl.HasChildNodes)
            {
                List<string> user = new List<string> { };
                foreach (XmlNode node in rootEl.ChildNodes)
                {
                    user = new List<string> { node.Attributes["name"].InnerText, node.Attributes["surname"].InnerText };
                    users.Add(user);
                }
            }
            return users;
        }
    }


    public class CsvAdapter : IUserRepository
    {
        private CsvFileReader _adaptee = null;

        public CsvAdapter(CsvFileReader adaptee)
        {
            _adaptee = adaptee;
        }

        public List<List<string>> GetUserNames()
        {
            string csvContent = _adaptee.ReadCsvFile("users.csv");

            string[] lines = csvContent.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            List<List<string>> users = new List<List<string>>();

            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(',');
                if (values.Length >= 2)
                {
                    users.Add(new List<string> { values[0], values[1] });
                }
            }

            return users;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            UsersApi usersRepository = new UsersApi();
            IUserRepository apiAdapter = new UsersApiAdapter(usersRepository);

            Console.WriteLine("Użytkownicy z API:");
            List<List<string>> apiUsers = apiAdapter.GetUserNames();
            int i = 1;
            apiUsers.ForEach(user => {
                string number = i >= 10 ? $"{i}" : $" {i}";
                Console.WriteLine($"{number}. {user[0]} {user[1]}");
                i++;
            });

            Console.WriteLine();


            CsvFileReader csvReader = new CsvFileReader();
            IUserRepository csvAdapter = new CsvAdapter(csvReader);

            Console.WriteLine("Użytkownicy z CSV:");
            List<List<string>> csvUsers = csvAdapter.GetUserNames();
            i = 1;
            csvUsers.ForEach(user => {
                string number = i >= 10 ? $"{i}" : $" {i}";
                Console.WriteLine($"{number}. {user[0]} {user[1]}");
                i++;
            });
        }
    }
}