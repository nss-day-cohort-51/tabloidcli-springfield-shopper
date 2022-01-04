using System;
using System.Collections.Generic;
using System.Text;

namespace TabloidCLI.UserInterfaceManagers
{
    public class ColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        private string _connectionString;

        public ColorManager(IUserInterfaceManager parentUI, string connectionString)
        {
            _parentUI = parentUI;
            _connectionString = connectionString;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Choose a background color");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("1) Red");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("2) Green");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("3) Yellow");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("4) White");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("5) Gray");
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("6) Blue");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("7) Cyan");
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("8) Magenta");
            Console.ResetColor();
            Console.WriteLine("0) Default");

            Console.Write("> ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    return _parentUI;

                case "2":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    return _parentUI;

                case "3":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    return _parentUI;

                case "4":
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    return _parentUI;

                case "5":
                    Console.BackgroundColor = ConsoleColor.Gray;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return _parentUI;

                case "6":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    return _parentUI;

                case "7":
                    Console.BackgroundColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    return _parentUI;

                case "8":
                    Console.BackgroundColor = ConsoleColor.Magenta;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    return _parentUI;

                case "0":
                    return _parentUI;

                default:
                    Console.WriteLine("Invalid Selection");
                    return this;
            };
        }
    }
   
}
