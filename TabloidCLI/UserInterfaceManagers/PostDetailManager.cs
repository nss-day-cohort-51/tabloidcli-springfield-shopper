using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Models;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;

namespace TabloidCLI.UserInterfaceManagers
{
    class PostDetailManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private AuthorRepository _authorRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _postId;

        public PostDetailManager(IUserInterfaceManager parentUI, string connectionString, int postId)
        {
            _parentUI = parentUI;
            _authorRepository = new AuthorRepository(connectionString);
            _postRepository = new PostRepository(connectionString);
            _tagRepository = new TagRepository(connectionString);
            _postId = postId;
        }

        public IUserInterfaceManager Execute()
        {
            Post post = _postRepository.Get(_postId);
            Console.WriteLine($"{post.Title} Details");
            Console.WriteLine(" 1) View");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Remove Tag");
            Console.WriteLine(" 0) Go Back");

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
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }

        private void View()
        {
            Post postDetail = _postRepository.Get(_postId);

            Console.WriteLine($"----------------------------------------------------------------");

            Console.WriteLine($"{postDetail.Title} ({postDetail.Url}) {postDetail.PublishDateTime}");

            Console.WriteLine($"----------------------------------------------------------------");
        }

        private void AddTag()
        {
            Post selectedPost = _postRepository.Get(_postId);

            Console.WriteLine($"----------------------------------------------------------------");
            Console.WriteLine("Which tag would you like to add? ");
            Console.WriteLine($"----------------------------------------------------------------");

            Console.WriteLine();

            List<Tag> allTags = _tagRepository.GetAll();

            for(int i = 0; i < allTags.Count; i++)
            {
                Tag tag = allTags[i];
                Console.WriteLine($" {i + 1}) {tag.Name}");
            }

            Console.Write("> ");

            string input = Console.ReadLine();

            try
            {
                int choice = int.Parse(input);
                Tag selectedTag = allTags[choice - 1];
                _postRepository.InsertTag(selectedPost, selectedTag);
            }
            catch
            {
                Console.WriteLine("Invalid Selection");
            }
        }

        private void RemoveTag()
        {
            throw new NotImplementedException();
        }

        private void NoteManagement()
        {
            throw new NotImplementedException();
        }
    }
}
