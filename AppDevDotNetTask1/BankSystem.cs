using System;
using System.Collections.Generic;
using System.IO;

namespace AppDevDotNetTask1
{
    class BankSystem
    {
        private List<Account> _accounts;

        public BankSystem()
        {
            _accounts = Input.GetAccountsFromFile();
        }

        /// <summary>
        /// Displays the main menu for the application
        /// </summary>
        public void MainMenu()
        {
            // As long as the user doesn't want to exit the application, keep looping every time a user visits
            // the menu, present them with their options.
            int selectionValue = 0;
            while (selectionValue != 7)
            {
                Console.Clear();
                Output.PrintWindow("WELCOME TO SIMPLE BANKING SYSTEM", "", new string[] {
                    "1. Create a new account",
                    "2. Search for an account",
                    "3. Deposit",
                    "4. Withdraw",
                    "5. A/C statement",
                    "6. Delete account",
                    "7. Exit"
                }, "Enter your choice (1-7):");

                Console.SetCursorPosition(32, 11);
                string selection = Console.ReadLine();

                Console.SetCursorPosition(0, 14);

                // Depending on the input by the user, display the relevant menu
                if (int.TryParse(selection, out selectionValue) && selectionValue > 0 && selectionValue <= 7)
                {
                    switch (selectionValue)
                    {
                        case 1:
                            CreateAccountMenu();
                            break;
                        case 2:
                            Console.Clear();
                            SearchAccountMenu();
                            break;
                        case 3:
                            DepositMenu();
                            break;
                        case 4:
                            WithdrawMenu();
                            break;
                        case 5:
                            StatementMenu();
                            break;
                        case 6:
                            DeleteAccountMenu();
                            break;
                        case 7:
                            Console.Write("Press any key to exit...");
                            Console.ReadKey();
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Please enter a valid selection (1-7)");
                    Console.WriteLine("Press any key to try again...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Display the menu to create a new account
        /// </summary>
        private void CreateAccountMenu()
        {
            Console.Clear();
            Output.PrintWindow("CREATE A NEW ACCOUNT", "ENTER THE DETAILS", new string[] {
                "First Name:",
                "Last Name:",
                "Address:",
                "Phone:",
                "Email:"
            });

            // Get the respective new account details from the user
            Console.SetCursorPosition(19, 5);
            string firstName = Console.ReadLine();

            Console.SetCursorPosition(18, 6);
            string lastName = Console.ReadLine();

            Console.SetCursorPosition(16, 7);
            string address = Console.ReadLine();

            Console.SetCursorPosition(14, 8);
            int phoneNumber = Input.PositiveIntegerInput();

            Console.SetCursorPosition(14, 9);
            string email = Console.ReadLine();

            Console.SetCursorPosition(0, 12);

            // Confirm the inputted fields meet the relevant criteria. Using logical '&' because I want
            // every field to validate, no point having validation appear one at a time
            if (Validator.Validate(firstName, "First Name", 30, 2) &
                Validator.Validate(lastName, "Last Name", 30, 2) &
                Validator.Validate(address, "Address", 60, 5) &
                Validator.Validate(phoneNumber, "Phone Number", 10, 6) &
                Validator.ValidateEmail(email, "Email"))
            {
                // If the entered info is valid & correct, create the new account
                bool informationCorrect = Input.YesNoInput("Is the information correct");
                if (informationCorrect)
                {
                    Account account = new Account(firstName, lastName, address, email, phoneNumber, Input.GetLatestAccountNumber(), 0, new List<Transaction>());

                    // Send the registration email to the new accounts email
                    if (account.SendRegistrationEmail())
                    {
                        Console.WriteLine("\nAccount created! Details will be provided via email.\n");
                        Console.WriteLine("Account number is: " + account.AccountNumber);
                        _accounts.Add(account);
                    }
                    else
                    {
                        Console.WriteLine("An error occured sending the registration email.");
                    }

                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
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

        /// <summary>
        /// Display the search account menu
        /// </summary>
        private void SearchAccountMenu()
        {
            Output.PrintWindow("SEARCH AN ACCOUNT", "ENTER THE DETAILS", new string[] {
                "Account Number:"
            });

            // Get the account id from the user, lookup the object & if it's not null, display the details
            Account account = AccountInputMenu();
            if (account != null)
            {
                account.PrintAccountDetails();
            }

            // If the user wants to search for another account, run this function again
            bool searchAgain = Input.YesNoInput("Check another account");
            Console.WriteLine("");
            if (searchAgain) SearchAccountMenu();
        }

        /// <summary>
        /// Display the deposit menu to add money to an account
        /// </summary>
        private void DepositMenu()
        {
            Console.Clear();
            Output.PrintWindow("DEPOSIT", "ENTER THE DETAILS", new string[] {
                "Account Number:",
                "Amount: $"
            });

            // Get the deposit amount as well as the account from the user
            var menuInput = AccountWithAmountMenu();
            int depositAmount = menuInput.Item1;
            Account account = menuInput.Item2;

            // Check the input was valid & the account exists
            if (account != null && Validator.Validate(depositAmount, "Deposit Amount"))
            {
                // Deposit the money into the account
                account.Deposit(depositAmount);
                Console.WriteLine("Deposit successful!");
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // If the user wants to deposity into another account, run this function again
            bool retry = Input.YesNoInput("Retry");
            if (retry) DepositMenu();
        }

        /// <summary>
        /// Display the withdrawl menu to take money from an account
        /// </summary>
        private void WithdrawMenu()
        {
            Console.Clear();
            Output.PrintWindow("WITHDRAW", "ENTER THE DETAILS", new string[] {
                "Account Number:",
                "Amount: $"
            });

            // Get the withdrawl amount as well as the account from the user
            var menuInput = AccountWithAmountMenu();
            int withdrawAmount = menuInput.Item1;
            Account account = menuInput.Item2;

            // Check the input was valid & the account exists
            if (account != null && Validator.Validate(withdrawAmount, "Withdrawl Amount"))
            {
                // Check the account has a large enough balance to withdraw
                if (account.Withdraw(withdrawAmount))
                {
                    Console.WriteLine("Withdrawl successful!");
                    Console.Write("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                else
                {
                    Console.WriteLine("Account does not have a sufficient balance to withdraw that amount.");
                }

            }

            // If the user wants to withdraw from another account, run this function again
            bool retry = Input.YesNoInput("Retry");
            if (retry) WithdrawMenu();
        }

        /// <summary>
        /// Display the menu to lookup an accounts statements & email them
        /// </summary>
        private void StatementMenu()
        {
            Console.Clear();
            Output.PrintWindow("STATEMENT", "ENTER THE DETAILS", new string[] {
                "Account Number:"
            });

            // Get the account based on the ID inputted by the user & check it belongs to a valid account
            Account account = AccountInputMenu();
            if (account != null)
            {
                Console.WriteLine("Account found! The statement is displayed below...");

                // Print the account statement
                account.PrintAccountDetails(true);

                // Check if the user wants an email sent
                bool email = Input.YesNoInput("Email statement");
                if (email)
                {
                    // Send an email to the accounts email address containing the acccounts statements
                    if (account.SendStatementEmail())
                    {
                        Console.WriteLine();
                        Console.WriteLine("Email sent succesfully!...");
                    }
                    else
                    {
                        Console.WriteLine();
                        Console.WriteLine("An error occurred whilst emailing the statement.");
                    }
                }

                Console.WriteLine();
                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // If the user wants to view another accounts statement, run this function again
            bool retry = Input.YesNoInput("Retry");
            if (retry) StatementMenu();
        }

        /// <summary>
        /// Display the menu to delete an account by it's ID
        /// </summary>
        private void DeleteAccountMenu()
        {
            Console.Clear();
            Output.PrintWindow("DELETE AN ACCOUNT", "ENTER THE DETAILS", new string[] {
                "Account Number:"
            });

            // Get the account based on it's ID which is taken from the user, check it exists & is valid
            Account account = AccountInputMenu();
            if (account != null)
            {
                Console.WriteLine("Account found! Details displayed below...");

                // Print the account details
                account.PrintAccountDetails();

                // Confirm that the user wants to delete this account
                bool delete = Input.YesNoInput("Delete");
                Console.WriteLine();

                // Try & locate this specific's account file & delete it, also remove the account from the local accounts list
                if (delete)
                {
                    try
                    {
                        File.Delete($"accounts/{account.AccountNumber}.txt");
                        _accounts.Remove(account);

                        Console.WriteLine("Account Deleted!...");
                    }
                    catch
                    {
                        Console.WriteLine("An error has occured trying to delete this account.");
                    }

                }

                Console.Write("Press any key to continue...");
                Console.ReadKey();
                return;
            }

            // If the user wants to delete another account, run this function again
            bool retry = Input.YesNoInput("Retry");
            if (retry) DeleteAccountMenu();
        }

        /// <summary>
        /// This function, returns an Accounts object based on it's ID if it exists
        /// </summary>
        /// <param name="id">The ID of the account to return</param>
        /// <returns>The account belonging to the given ID if it exists</returns>
        private Account AccountByID(int id)
        {
            foreach (Account account in _accounts)
            {
                if (account.AccountNumber == id) return account;
            }
            return null;
        }

        /// <summary>
        /// Retrieves an account number & amount from the user whilst performing validation.
        /// </summary>
        /// <returns>A tuple containing the amount entered & the account with the ID entered by the user.</returns>
        private (int, Account) AccountWithAmountMenu()
        {
            // Get the account ID from the user
            Console.SetCursorPosition(23, 5);
            int enteredAccountID = Input.PositiveIntegerInput();

            // Reset cursor to be below the box for any future output
            Console.SetCursorPosition(0, 8);

            // Search for the account based on the ID entered
            Account account = AccountByID(enteredAccountID);

            // Validate the input & check if the account was found
            if (Validator.Validate(enteredAccountID, "Account Number", 10, 6) && account == null)
            {
                Console.WriteLine("Account not found");
                return (-1, null);
            }
            // If the account was found, ask the user for an amount
            else if (account != null)
            {
                Console.WriteLine("Account found! Enter the amount...");

                // Get the amount from the user
                Console.SetCursorPosition(16, 6);
                int amount = Input.PositiveIntegerInput();

                Console.SetCursorPosition(0, 9);

                return (amount, account);
            }

            return (-1, null);
        }

        /// <summary>
        /// Asks the user for an account number & verifies it exists.
        /// </summary>
        /// <returns>Returns the inputted account number if it exists.</returns>
        private Account AccountInputMenu()
        {
            // Get the account ID from the user
            Console.SetCursorPosition(23, Console.CursorTop - 2);
            int enteredAccountID = Input.PositiveIntegerInput();

            // Reset cursor position to be below the box
            Console.SetCursorPosition(0, Console.CursorTop + 2);

            // Check if the account exists
            Account account = AccountByID(enteredAccountID);

            // Validate the input & whether the account exists
            if (Validator.Validate(enteredAccountID, "Account Number", 10, 6) == false || account == null)
            {
                Console.WriteLine("Account not found");
            }
            else
            {
                return account;
            }

            return null;
        }

    }
}