using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TabloidCLI.Repositories;
using TabloidCLI.Models;


namespace TabloidCLI.UserInterfaceManagers
{
    internal class BlogDetailsManager : IUserInterfaceManager
    {
        private IUserInterfaceManager _parentUI;
        private BlogRepository _blogRepository;
        private PostRepository _postRepository;
        private TagRepository _tagRepository;
        private int _blogId;

    public BlogDetailsManager(IUserInterfaceManager parentUI, string connectionString, int blogId)
    {
        _parentUI = parentUI;
        _blogRepository = new BlogRepository(connectionString);
        _postRepository = new PostRepository(connectionString);
        _tagRepository = new TagRepository(connectionString);
        _blogId = blogId;
    }
        public IUserInterfaceManager Execute()
        {
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine($"{blog.Title} Details");
            Console.WriteLine("1) View");
            Console.WriteLine("2) Add Tag");
            Console.WriteLine("3) Remove Tag");
            Console.WriteLine("4) View Post");
            Console.WriteLine("0) Return");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                   View();
                    return this;
                case "2":
                  //
                    return this;
                case "3":
                    //
                    return this;
                case "4":
                    //
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
            Blog blog = _blogRepository.Get(_blogId);
            Console.WriteLine("Blog Details");
            Console.WriteLine($"Blog's name: {blog.Title}");
            Console.WriteLine($"Blog's url: {blog.Url}");

        }




    }
}
