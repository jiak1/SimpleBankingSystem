using System;
using System.Collections.Generic;
using System.IO;

namespace AppDevDotNetTask1
{
    class LoginSystem
    {
        private BankSystem _accountSystem;

        public LoginSystem()
        {
            _accountSystem = new BankSystem();
        }

        public void LoginMenu()
        {
            // Display the login menu with the relevant fields
            Output.PrintWindow(
                "WELCOME TO SIMPLE BANKING SYSTEM",
                "LOGIN TO START",
                new string[] {
                    "User Name:",
                    "Password:"
                });

            // Read the username & password from the user
            Console.SetCursorPosition(18, 5);
            string username = Console.ReadLine();

            Console.SetCursorPosition(17, 6);
            string password = Input.ReadMaskedInput();

            Console.SetCursorPosition(0, 9);

            if (File.Exists("login.txt") == false)
            {
                Console.WriteLine("Cannot find login.txt");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
                return;
            }

            try
            {
                // Loop through all the lines in login.txt & stick the username & password
                // into the credentials dictionary.
                Dictionary<string, string> credentials = new Dictionary<string, string>();

                string[] lines = File.ReadAllLines("login.txt");
                foreach (string credentialLine in lines)
                {
                    credentials[credentialLine.Split("|")[0]] = credentialLine.Split("|")[1];
                }

                // Check if the inputted username exists in the credentials dict, if so
                // confirm the password entered was correct before moving to the main menu.
                if (credentials.ContainsKey(username) && credentials[username] == password)
                {
                    _accountSystem.MainMenu();
                }
                else
                {
                    Console.WriteLine("Invalid credentials");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                    Console.Clear();
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
    }
}