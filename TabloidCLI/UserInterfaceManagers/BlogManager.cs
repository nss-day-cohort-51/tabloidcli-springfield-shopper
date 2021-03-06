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
            Console.WriteLine("Blog Menu");
            Console.WriteLine(" 1) List Blogs");
            Console.WriteLine("2) View Details");
            Console.WriteLine(" 3) Add Blog");
            Console.WriteLine(" 4) Edit Blog");
            Console.WriteLine(" 5) Remove Blog");
            Console.WriteLine(" 6) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    List();
                    return this;

                case "2":
                    Blog blog = Choose();
                    if (blog == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new BlogDetailManager(this, _connectionString, blog.Id);
                    }

                case "3":
                    Add();
                    return this;

                case "4":
                    Edit();
                    return this;

                case "5":
                    Remove();
                    return this;

                case "6":
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

        private void Edit()
        {

            Blog blogToEdit = Choose("Which blog would you like to edit?");
            if (blogToEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("New Title (blank to leave unchanged: ");
            string title = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(title))
            {
                blogToEdit.Title = title;
            }

            Console.WriteLine();
            Console.Write("New Url (blank to leave unchanged: ");
            string url = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(url))
            {
                blogToEdit.Url = url;
            }
           
            _blogRepository.Update(blogToEdit);
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
