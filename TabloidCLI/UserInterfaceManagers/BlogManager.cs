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
                    Remove();
                    return this;

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

        private Blog Choose(string prompt = null)
        {
            if(prompt == null)
            {
                prompt = "Please choose a Blog";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for(int i = 0; i < blogs.Count; i++)
            {
                Console.WriteLine($" {i + 1}) {blogs[i].Title}");
            }

            Console.Write("> ");

            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch(Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }

        }

        private void Remove()
        {
            Blog selectedBlog = Choose();

            if(selectedBlog != null)
            {
                _blogRepository.Delete(selectedBlog.Id);
            }


        }
    }
}
