using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
     public class PostManager : IUserInterfaceManager

    {
           private readonly IUserInterfaceManager _parentUI;
           private string _connectionString;
           private PostRepository _postRepository;
           private AuthorRepository _authorRepository;
           private BlogRepository _blogRepository;


        public PostManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
            _postRepository = new PostRepository(connectionString);
            _authorRepository = new AuthorRepository(connectionString);
            _blogRepository = new BlogRepository(connectionString);
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
            switch (choice)
            {
                case "1":
                   // List();
                    return this;
                case "2":
                    Add();
                    return this;
                case "3":
                   // Edit()
                    return this;
                case "4":
                   // Remove();
                    return this;
                case "5":
                   //  Note management();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }

        }

        private void Add ()
        {
            Console.WriteLine("New Post");

            Post post = new Post();
            
            Console.WriteLine("Title of Post");
            post.Title = Console.ReadLine();

            Console.WriteLine("Url of Post");
            post.Url = Console.ReadLine();

            
            post.PublishDateTime =  DateTime.Now;

            Console.WriteLine("Enter Your AuthorId");
            List<Author> authors = _authorRepository.GetAll();
            foreach (Author author in authors)
            {
                Console.WriteLine($"Author's name: {author.FirstName}, Id: {author.Id}");
            }
            Console.WriteLine("Choose one");
            post.Author = new Author { Id =  int.Parse(Console.ReadLine())}; 
          

            Console.WriteLine("Enter BlogId");
            List<Blog> blogs =  _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"Blog's title is {blog.Title}, Id # {blog.Id}");
            }
            post.Blog = new Blog { Id = int.Parse(Console.ReadLine())};

             _postRepository.Insert(post);
           
           
        }


    }
}
