using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogManager : IUserInterfaceManager
    {

        private readonly IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private string _connectionString;

        public BlogManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _blogRepository = new BlogRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine(" 2) Add Blog");
            Console.WriteLine(" 3) Edit Blog");
            Console.WriteLine(" 4) Remove Blog");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;

                case "2":
                    Add();
                    return this;

                case "3":
                    throw new NotImplementedException();

                case "4":
                    throw new NotImplementedException();

                case "5":
                    throw new NotImplementedException();

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            };


       }

        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.Write("Enter New Title > ");
            blog.Title = Console.ReadLine();

            Console.Write("Enter new Url > ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);
        }

        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();

            foreach(Blog b in blogs)
            {
                Console.WriteLine("-----------");
                Console.WriteLine(b);

            }
        }
    }
}
