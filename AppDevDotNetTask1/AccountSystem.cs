using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace AppDevDotNetTask1
{
    class AccountSystem
    {
        private List<Account> accounts = new List<Account>();

        public AccountSystem()
        {
            LoadAccountsFromFile();
        }

        public void LoadAccountsFromFile()
        {
            if(Directory.Exists("accounts") == false)
            {
                Directory.CreateDirectory("accounts");
            }

            //string[] accountFiles = Directory.GetFiles("accounts");

        }

        public int LatestAccountNumber()
        {
            string[] accountFiles = Directory.GetFiles("accounts");
            int previousID = 100000;

            foreach (string accountFile in accountFiles)
            {
                try
                {
                    int accountID = int.Parse(accountFile.Replace("accounts\\", "").Split(".")[0]);
                    if(accountID > previousID)
                    {
                        previousID = accountID+1;
                    }
                }
                catch { }
            }

            return previousID + 1;
        }

        public void MainMenu()
        {
            int selectionValue = 0;
            while (selectionValue != 7)
            {
                PrintMainMenu();

                Console.SetCursorPosition(32, 11);
                string selection = Console.ReadLine();

                Console.SetCursorPosition(0, 14);

                if (int.TryParse(selection, out selectionValue) && selectionValue > 0 && selectionValue <= 7)
                {
                    switch (selectionValue)
                    {
                        case 1:
                            CreateAccountMenu();
                            break;
                        case 7:
                            Console.Write("Press any key to exit...");
                            Console.ReadLine();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection (1-7)");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadLine();
                    MainMenu();
                }
            }
        }

        private void CreateAccountMenu()
        {
            PrintCreateMenu();

            Console.SetCursorPosition(19, 5);
            string firstName = Console.ReadLine();

            Console.SetCursorPosition(18, 6);
            string lastName = Console.ReadLine();

            Console.SetCursorPosition(16, 7);
            string address = Console.ReadLine();

            Console.SetCursorPosition(14, 8);
            string phone = Console.ReadLine();

            Console.SetCursorPosition(14, 9);
            string email = Console.ReadLine();

            Console.SetCursorPosition(0, 12);

            bool valid = true;

            if(firstName.Length < 2)
            {
                Console.WriteLine("First name must be at least 2 characters long.");
                valid = false;
            }

            if(lastName.Length < 2)
            {
                Console.WriteLine("Last name must be at least 2 characters long.");
                valid = false;
            }

            if(address.Length < 2)
            {
                Console.WriteLine("Address must be at least 2 characters long.");
                valid = false;
            }

            int phoneNumber = 0;

            if(phone.Length < 6 || phone.Length > 10)
            {
                Console.WriteLine("Phone number must be between 6-10 numbers long.");
                valid = false;
            }
            else if (int.TryParse(phone, out phoneNumber) == false)
            {
                Console.WriteLine("Phone number is not numeric.");
                valid = false;
            }

            if (email.Length < 2)
            {
                Console.WriteLine("Email must be at least 2 characters long.");
                valid = false;
            }
            else
            {
                if (email.Contains("@"))
                {
                    string[] emailSplit = email.Split("@");
                    if (emailSplit.Length > 1 && emailSplit[1].Length <= 0)
                    {
                        Console.WriteLine("Email must have a valid domain.");
                        valid = false;
                    }
                }
                else
                {
                    Console.WriteLine("Email must contain an '@' symbol.");
                    valid = false;
                }
            }

            if (valid)
            {
                string response = "";
                while (response != "y" && response != "n")
                {
                    Console.Write("Is the information correct (y/n)? ");
                    response = Console.ReadLine();
                    if(response != "y" && response != "n")
                    {
                        Console.WriteLine("Invalid input, enter either 'y' or 'n'.\n");
                    }
                }

                if (response == "y")
                {
                    Account account = new Account(firstName, lastName, address, email, phoneNumber, LatestAccountNumber());
                    accounts.Add(account);

                    Console.WriteLine("\nAccount created! Details will be provided via email.\n");
                    Console.WriteLine("Account number is: " + account.accountNumber);
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine("Press any key to re-enter the correct information...");
                    Console.ReadKey();
                    CreateAccountMenu();
                }
            }
            else
            {
                Console.WriteLine("Press any key to retry...");
                Console.ReadKey();
                CreateAccountMenu();
            }
        }

        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║        WELCOME TO SIMPLE BANKING SYSTEM        ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║      1. Create a new account                   ║");
            Console.WriteLine("║      2. Search for an account                  ║");
            Console.WriteLine("║      3. Deposit                                ║");
            Console.WriteLine("║      4. Withdraw                               ║");
            Console.WriteLine("║      5. A/C statement                          ║");
            Console.WriteLine("║      6. Delete account                         ║");
            Console.WriteLine("║      7. Exit                                   ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║      Enter your choice (1-7):                  ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintCreateMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              CREATE A NEW ACCOUNT              ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║                ENTER THE DETAILS               ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      First Name:                               ║");
            Console.WriteLine("║      Last Name:                                ║");
            Console.WriteLine("║      Address:                                  ║");
            Console.WriteLine("║      Phone:                                    ║");
            Console.WriteLine("║      Email:                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintSearchMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║               SEARCH AN ACCOUNT                ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintAccountDetails()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                ACCOUNT DETAILS                 ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account No:                               ║");
            Console.WriteLine("║      Account Balance:                          ║");
            Console.WriteLine("║      First Name:                               ║");
            Console.WriteLine("║      Last Name:                                ║");
            Console.WriteLine("║      Address:                                  ║");
            Console.WriteLine("║      Phone:                                    ║");
            Console.WriteLine("║      Email:                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintDepositMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                    DEPOSIT                     ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("║      Amount:                                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintWithdrawMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                   WITHDRAW                     ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("║      Amount:                                   ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintStatementMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║                   STATEMENT                    ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintAccountStatement()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║              SIMPLE BANKING SYSTEM             ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║      Account Statement                         ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account No:                               ║");
            Console.WriteLine("║      Account Balance:                          ║");
            Console.WriteLine("║      First Name:                               ║");
            Console.WriteLine("║      Last Name:                                ║");
            Console.WriteLine("║      Address:                                  ║");
            Console.WriteLine("║      Phone:                                    ║");
            Console.WriteLine("║      Email:                                    ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }

        private void PrintDeleteAccountMenu()
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════════════╗");
            Console.WriteLine("║               DELETE AN ACCOUNT                ║");
            Console.WriteLine("╠════════════════════════════════════════════════╣");
            Console.WriteLine("║               ENTER THE DETAILS                ║");
            Console.WriteLine("║                                                ║");
            Console.WriteLine("║      Account Number:                           ║");
            Console.WriteLine("╚════════════════════════════════════════════════╝");
        }
    }
}
