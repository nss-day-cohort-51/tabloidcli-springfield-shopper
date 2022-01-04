using System;
using System.Collections.Generic;
using System.Text;
using TabloidCLI.Repositories;
using TabloidCLI.UserInterfaceManagers;
using System.Linq;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class PostManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private PostRepository _postRepository;
        private AuthorRepository _authorRepository;
        private BlogRepository _blogRepository;
        
        private AuthorManager _authorManager;
        private BlogManager _blogManager;
        private string _connectionString;

        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
            _authorManager = new AuthorManager(this,connectionString);
            _blogManager = new BlogManager(this, connectionString);
            
            _connectionString = connectionString;
        }
        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Post Menu");
            Console.WriteLine(" 1) List Posts");
            Console.WriteLine(" 2) Add Post");
            Console.WriteLine(" 3) Edit Post");
            Console.WriteLine(" 4) Remove Post");
            Console.WriteLine(" 5) Note Management");
            Console.WriteLine(" 0) Return to Main Menu");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch(choice)
            {
                case "1":
                    List();
                    return this;

                case "2":
                    Add();
                    return this;

                case "3":
                    Edit();
                    return this;
                    
                case "4":
                    Remove();
                    return this;
                    
                case "5":
                    Post post = Choose();
                    if (post == null)
                    {
                        return this;
                    }
                    else
                    {
                        return new PostDetailManager(this, _connectionString, post.Id);
                    }

                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            };
        }

        private void List()
        {
            List<Post> getPosts = _postRepository.GetAll();

            foreach(Post p in getPosts)
            {
                Console.WriteLine(p);
            }
        }

        private Post Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Post:";
            }

            Console.WriteLine(prompt);

            List<Post> posts = _postRepository.GetAll();

            for (int i = 0; i < posts.Count; i++)
            {
                Post post = posts[i];
                Console.WriteLine($" {i + 1}) {post.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return posts[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Author ChooseAuthor(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose an Author:";
            }

            Console.WriteLine(prompt);

            List<Author> authors = _authorRepository.GetAll();

            for (int i = 0; i < authors.Count; i++)
            {
                Author author = authors[i];
                Console.WriteLine($" {i + 1}) {author.FullName}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return authors[choice - 1];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }

        private Blog ChooseBlog(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
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
            catch (Exception ex)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }

        }

        private void Add()
        {
            Post newPost = new Post();

            Console.Write("Please enter post title: ");
            string postTitle = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(postTitle))
            {
                newPost.Title = postTitle;
            }

            Console.Write("Please enter post url: ");
            string postUrl = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(postUrl))
            {
                newPost.Url = postUrl;
            }

            DateTime postDate = DateTime.Now;

            newPost.PublishDateTime = postDate;
            
            newPost.Author = ChooseAuthor();

            newPost.Blog = ChooseBlog();

            _postRepository.Insert(newPost);

        }

        private void Remove()
        {
            Post deletePost = Choose();

            if(deletePost != null)
            {
                _postRepository.Delete(deletePost.Id);
            }
        }

        private void Edit()
        {
            Post postEdit = Choose("Choose a post to edit: ");

            if(postEdit == null)
            {
                return;
            }

            Console.WriteLine();
            Console.Write("Please enter new post title: ");
            string postTitle = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(postTitle))
            {
                postEdit.Title = postTitle;
            }

            Console.Write("Please enter post url: ");
            string postUrl = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(postUrl))
            {
                postEdit.Url = postUrl;
            }

            DateTime postDate = DateTime.Now;

            postEdit.PublishDateTime = postDate;

            postEdit.Author = ChooseAuthor();

            postEdit.Blog = ChooseBlog();

            _postRepository.Update(postEdit);
        }
         
    }
}
