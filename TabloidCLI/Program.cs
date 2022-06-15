using System;
using TabloidCLI.UserInterfaceManagers;



namespace TabloidCLI
{
    class Program
    {
        static void Main(string[] args)
        {

            // MainMenuManager implements the IUserInterfaceManager interface
            IUserInterfaceManager ui = new MainMenuManager();
            Console.WriteLine("Hello! have a pleasant day");
            Console.WriteLine("--------------------------");
            Console.WriteLine(@"Lets put some color in your console.
1 - Gray
2 - Blue
3 - Red
4 - Yellow");
            string numForBackground = Console.ReadLine();

            switch (numForBackground)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.Clear();
                    break;

                case "2":
                    Console.BackgroundColor = ConsoleColor.DarkCyan;
                    Console.Clear();
                    break;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.Clear();
                    break;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Something went wrong, no color was chosen.");
                    break;
            }

            while (ui != null)
            { 
                // Each call to Execute will return the next IUserInterfaceManager we should execute
                // When it returns null, we should exit the program;
                ui = ui.Execute();
            }
        }
    }
}
