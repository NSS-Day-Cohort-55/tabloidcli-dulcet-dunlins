using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class JournalManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private JournalRepository _journalRepository;
        private string _connectionString;

        public JournalManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _journalRepository = new JournalRepository(connectionString);
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine(" 1) List Journal Entries");
            Console.WriteLine(" 2) Add Journal Entry");
            Console.WriteLine(" 0) Go Back");

            Console.WriteLine("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List<Journal> allEntries = _journalRepository.GetAll();
                    foreach(Journal journal in allEntries)
                    {
                        Console.WriteLine($"{journal.Title}{journal.Content} {journal.CreateDateTime}");
                    }
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                    //Delete();
                    //return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        public void Add()
        {
            Journal journalObject = new Journal();
            Console.Write("Please enter journal title: ");
            journalObject.Title = Console.ReadLine();

            Console.Write("Please enter journal content: ");
            journalObject.Content = Console.ReadLine();

            journalObject.CreateDateTime = DateTime.Now;

            _journalRepository.Insert(journalObject);
            
        }

        public void Delete()
        {
            List<Journal> journals =  _journalRepository.GetAll();
            for

        }

    }
}
