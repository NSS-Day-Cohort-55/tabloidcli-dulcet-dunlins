using System;
using System.Collections.Generic;
using TabloidCLI.Models;
using TabloidCLI.Repositories;

namespace TabloidCLI.UserInterfaceManagers
{
    public class BlogManager : IUserInterfaceManager
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
            Console.WriteLine(" 2) Add Blog");
            Console.WriteLine(" 3) Edit Blog");
            Console.WriteLine(" 4) Remove Blog");
            Console.WriteLine(" 0) Go Back");

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
                    Edit();
                    return this;
                case "4":
                    Remove();
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            }
        }
        private void List()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine(blog);
            }
        }

        private void Add()
        {
            Console.WriteLine("New Blog");
            Blog blog = new Blog();

            Console.WriteLine("Title: ");
            blog.Title = Console.ReadLine();

            Console.WriteLine("Url: ");
            blog.Url = Console.ReadLine();

            _blogRepository.Insert(blog);

        }

        private void Edit()
        {
            List<Blog> blogs = _blogRepository.GetAll();
            foreach (Blog blog in blogs)
            {
                Console.WriteLine($"{blog.Id} - {blog.Title}, {blog.Url}");
            }
            Console.WriteLine("Which one would you like to update?");
            int blogIdToUpdate = int.Parse(Console.ReadLine());
            Console.WriteLine("What would you like the new title to be?");
            string blogTitle = Console.ReadLine();
            Console.WriteLine("What would you like the new url to be?");
            string blogUrl = Console.ReadLine();
            Blog updatedBlogOBj = new Blog()
            {
                Id = blogIdToUpdate,
                Title = blogTitle,
                Url = blogUrl   
            };
            _blogRepository.Update(updatedBlogOBj);
            Console.WriteLine("Your blog has been updated.");
        }

        private void Remove()
        {
            Blog blogToDelete = Choose("Which Blog would you like to remove?");
            if (blogToDelete != null)
            {
                _blogRepository.Delete(blogToDelete.Id);
            }
        }

        private Blog Choose(string prompt = null)
        {
            if (prompt == null)
            {
                prompt = "Please choose a Blog:";
            }

            Console.WriteLine(prompt);

            List<Blog> blogs = _blogRepository.GetAll();

            for (int i = 0; i < blogs.Count; i++)
            {
                Blog blog = blogs[i];
                Console.WriteLine($" {i + 1}) {blog.Title}");
            }
            Console.Write("> ");

            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                return blogs[choice - 1];
            }
            catch (Exception)
            {
                Console.WriteLine("Invalid Selection");
                return null;
            }
        }
    }


}