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
                    List();
                    return this;

                case "2":
                    throw new NotImplementedException();

                case "3":
                    Add();
                    return this;

                case "4":
                    Edit();
                    return this;

                case "5":
                    Remove();
                    return this;

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

        private void List()
        {
            List<Journal> journals = _journalRepository.GetAll();

            foreach(Journal j in journals)
            {
                Console.WriteLine("-------------");
                Console.WriteLine($"Title: {j.Title}");
                Console.WriteLine($"Entry Content: {j.Content}");
                Console.WriteLine($"{j.CreateDateTime}");
                Console.WriteLine("-------------");
            }
        }

        private Journal Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Entry:";
            }

            Console.WriteLine(prompt);

            List<Journal> journals = _journalRepository.GetAll();

            for (int i = 0; i < journals.Count; i++)
            {
                Journal journal = journals[i];
                Console.WriteLine($" {i + 1}) {journal.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return journals[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private void Remove()
        {
            Journal entryToDelete = Choose("Which entry would you like to remove?");
            if (entryToDelete != null)
            {
                _journalRepository.Delete(entryToDelete.Id);
            }
        }

        private void Edit()
        {
            Journal newJournal = Choose("Please choose a journal entry to edit: ");
            if(newJournal == null)
            {
                return;
            }

            Console.Write("Enter new title: ");
            string newTitle = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newTitle))
            {
                newJournal.Title = newTitle;
            }

            Console.Write("Enter new content: ");
            string newContent = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newContent))
            {
                newJournal.Content = newContent;
            }

            newJournal.CreateDateTime = DateTime.Now;

            _journalRepository.Update(newJournal);

        }


    }
}
