using System;
using System.IO;

namespace AppDevDotNetTask1
{
    class BankSystem
    {
        private AccountSystem accountSystem;

        public BankSystem()
        {
            accountSystem = new AccountSystem();
            accountSystem.MainMenu();
            //LoginMenu();
        }

        private void LoginMenu()
        {

            PrintLoginMenu();

            Console.SetCursorPosition(19, 5);
            string username = Console.ReadLine();

            Console.SetCursorPosition(18, 6);
            string password = ReadPrivateInput();

            Console.SetCursorPosition(0, 9);

            if (File.Exists("login.txt"))
            {
                bool validCredentials = false;

                try
                {
                    string[] lines = File.ReadAllLines("login.txt");
                    foreach (string credentialLine in lines)
                    {
                        string lineUsername = credentialLine.Split("|")[0];
                        string linePassword = credentialLine.Split("|")[1];

                        if (lineUsername == username && linePassword == password)
                        {
                            validCredentials = true;
                            break;
                        }
                    }

                    if (validCredentials)
                    {
                        accountSystem.MainMenu();
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentials");
                        Console.WriteLine("Press any key to try again...");
                        Console.ReadKey();
                        LoginMenu();
                    }
                }
                catch
                {
                    Console.WriteLine("login.txt is not formatted correctly.");
                    Console.WriteLine("Press any key to exit...");
                    Console.ReadKey();
                }
            }
            else
            {
                Console.WriteLine("Cannot find login.txt");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
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
                    if (input.Length > 0)
                    {
                        Console.Write("\b \b");
                        input = input.Remove(input.Length - 1);
                    }
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
