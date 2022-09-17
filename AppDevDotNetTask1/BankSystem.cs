using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppDevDotNetTask1
{
    class BankSystem
    {
        public BankSystem()
        {
            if (File.Exists("login.txt"))
            {
                Console.WriteLine("exists");
            }
            else
            {
                Console.WriteLine("doesnt exist");
            }
            Console.ReadKey();
            PrintLoginMenu();
            Console.SetCursorPosition(18, 5);
            string username = Console.ReadLine();
            Console.SetCursorPosition(17, 6);
            string password = ReadPrivateInput();
            Console.WriteLine(password);
        }

        private string ReadPrivateInput()
        {
            string input = "";
            ConsoleKeyInfo inputKey;
            while (true)
            {
                inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else if (inputKey.Key == ConsoleKey.Backspace)
                {
                    Console.Write("\b \b");
                    input = input.Remove(input.Length - 1);

                }
                else if (inputKey.Key != ConsoleKey.Escape && inputKey.Key != ConsoleKey.Spacebar && inputKey.Key != ConsoleKey.Tab)
                {
                    Console.Write("*");
                    input += inputKey.KeyChar;
                }
            }

            return input;
        }

        private void PrintLoginMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║        WELCOME TO SIMPLE BANKING SYSTEM        ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║                 LOGIN TO START                 ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      User Name:                                ║");
            Console.WriteLine("║      Password:                                 ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }
    }
}
