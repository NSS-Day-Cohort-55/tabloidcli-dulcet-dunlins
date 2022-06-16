using System;
using System.Collections.Generic;
using TabloidCLI.Models;

namespace TabloidCLI.UserInterfaceManagers
{
    public class TagManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private TagRepository _tagRepository;
        private string _connectionString;

        public TagManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _tagRepository = new TagRepository(connectionString);
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Tag Menu");
            Console.WriteLine(" 1) List Tags");
            Console.WriteLine(" 2) Add Tag");
            Console.WriteLine(" 3) Edit Tag");
            Console.WriteLine(" 4) Remove Tag");
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
            List<Tag> tagList = _tagRepository.GetAll();
            foreach(Tag tag in tagList)
            {
                Console.WriteLine($"{tag.Name}");
            }
        }

        private void Add()
        {
            Tag userTag = new Tag();
            Console.Write("Please enter tag name: ");
            userTag.Name = Console.ReadLine();

            _tagRepository.Insert(userTag);
        }

        private void Edit()
        {
            List<Tag> tagList = _tagRepository.GetAll();
            foreach (Tag tag in tagList)
            {
                Console.WriteLine($"{tag.Id}- {tag.Name}");
            }
            Console.WriteLine("Which tag would you like to edit?");
            int tagId = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter the new name. If you wish to not edit the name just press enter.");
            string newTagName = Console.ReadLine();
            if (string.IsNullOrEmpty(newTagName))
            {
                Console.WriteLine("The tag has not been edited.");
            }
            else
            {
                Tag editedInfo = new Tag();
                editedInfo.Name = newTagName;
                editedInfo.Id = tagId;
                _tagRepository.Update(editedInfo);
                Console.WriteLine("The tag was updated.");
            }
        }

        private void Remove()
        {
            throw new NotImplementedException();
        }
    }
}
