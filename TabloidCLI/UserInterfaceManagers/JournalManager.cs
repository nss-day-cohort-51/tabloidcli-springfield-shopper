using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;


namespace TabloidCLI.UserInterfaceManagers
{
    class JournalManager : IUserInterfaceManager
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
            Console.WriteLine(" 2) Journal Details");
            Console.WriteLine(" 3) Add Journal Entry ");
            Console.WriteLine(" 4) Edit Journal Entry ");
            Console.WriteLine(" 5) Remove Journal Entry ");
            Console.WriteLine(" 0) Go Back");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    throw new NotImplementedException();

                case "2":
                    throw new NotImplementedException();

                case "3":
                    Add();
                    return this;

                case "4":
                    throw new NotImplementedException();

                case "5":
                    throw new NotImplementedException();

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void Add()
        {
            Console.WriteLine("New Journal Entry");
            Journal journal = new Journal();

            Console.Write("Title? >");
            journal.Title = Console.ReadLine();

            Console.Write("Enter Content> ");
            journal.Content = Console.ReadLine();

            journal.CreateDateTime =  DateTime.Now;

            _journalRepository.Insert(journal);
        }

    }
}
