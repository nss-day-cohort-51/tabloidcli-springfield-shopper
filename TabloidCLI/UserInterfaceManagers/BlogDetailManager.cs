using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI.UserInterfaceManagers
{
    class BlogDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _blogId;

        public BlogDetailManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _blogId = blogId;
        }

        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine("1) View");
            Console.WriteLine("2) Add Tag");
            Console.WriteLine("3) Remove Tag");
            Console.WriteLine("4) View Posts");
            Console.WriteLine("0) Return");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    View();
                    return this;

                case "2":
                    AddTag();
                    return this;

                case "3":
                    RemoveTag();
                    return this;

                case "4":
                    ViewPosts();
                    return this;

                case "0":
                    return _parentUI;

                default:
                    Console.WriteLine("Invalid Selection");
                    return this;

            }


        }

            private void View()
        {
            Blog blogDetail = _blogRepository.Get(_blogId);

            Console.WriteLine("--------------------------");

            Console.WriteLine($"Title: {blogDetail.Title} Url: ({blogDetail.Url})");

            Console.WriteLine("--------------------------");
        }
            private void AddTag()
            {
                throw new NotImplementedException();
            }

            private void RemoveTag()
            {
                throw new NotImplementedException();
            }

            private void ViewPosts()
            {
                throw new NotImplementedException();
            }
    }
}
