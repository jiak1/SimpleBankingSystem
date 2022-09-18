using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

namespace AppDevDotNetTask1
{
    class Input
    {
        /// <summary>
        /// Prompts the user with a simple yes/no question & converts the response into a boolean.
        /// </summary>
        /// <param name="prompt">The prompt to display to the user</param>
        /// <returns>Whether the user responded with yes (true) or no (false)</returns>
        public static bool YesNoInput(string prompt)
        {
            // Continue prompting the user until they enter a valid value that is either 'y' or 'n'
            string response = "";
            while (response != "y" && response != "n")
            {
                Console.Write($"{prompt} (y/n)? ");

                // Check the response was either 'y' or 'n', otherwise prompt the user to try again
                response = Console.ReadLine().ToLower().Trim();
                if (response != "y" && response != "n")
                {
                    Console.WriteLine("Invalid input, enter either 'y' or 'n'.\n");
                }
            }

            return response == "y";
        }

        /// <summary>
        /// Get a numeric, positive number from the user.
        /// </summary>
        /// <returns>A number the user recieved or -1 if it was invalid.</returns>
        public static int PositiveIntegerInput()
        {
            string input = Console.ReadLine();

            // Try convert the input into a number, if it fails, return -1
            int output;
            if (int.TryParse(input, out output))
            {
                return output;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Read in masked input from the user.
        /// </summary>
        /// <returns>The unmasked value the user entered.</returns>
        public static string ReadMaskedInput()
        {
            // Keep track of the input value
            string input = "";
            ConsoleKeyInfo inputKey;

            // Keep reading input from the user unless they hit enter which breaks this loop
            while (true)
            {
                inputKey = Console.ReadKey(true);

                if (inputKey.Key == ConsoleKey.Enter)
                {
                    break;
                }
                // If the user hit's backspace, erase the previously printed '*' (If there is one) & also remove the
                // last character from our unmasked input variable
                else if (inputKey.Key == ConsoleKey.Backspace)
                {
                    if (input.Length > 0)
                    {
                        Console.Write("\b \b");
                        input = input.Remove(input.Length - 1);
                    }
                }
                // If the user hits any other alphanumeric key, print a '*' in it's place
                else if (inputKey.Key != ConsoleKey.Escape && inputKey.Key != ConsoleKey.Spacebar && inputKey.Key != ConsoleKey.Tab)
                {
                    Console.Write("*");
                    input += inputKey.KeyChar;
                }
            }

            return input;
        }

        /// <summary>
        /// This function reads all of the text files in the accounts folder & serialises them into a list of account objects
        /// </summary>
        public static List<Account> GetAccountsFromFile()
        {
            if (Directory.Exists("accounts") == false)
            {
                Directory.CreateDirectory("accounts");
            }

            List<Account> accounts = new List<Account>();

            // Get the names of all the files in the accounts folder & loop through them
            string[] accountFiles = Directory.GetFiles("accounts");
            foreach (string fileDir in accountFiles)
            {
                try
                {
                    // Try & read the account file
                    string[] accountFileLines = File.ReadAllLines(fileDir);

                    // Read everything prior to the transactions (Line 7) & store them in a dictionary where 
                    // the field name is the key e.g the key 'First Name' returns 'Jack'
                    Dictionary<string, string> accountDetails = new Dictionary<string, string>();
                    for (int i = 0; i < Math.Min(7, accountFileLines.Length); i++)
                    {
                        string[] lineSplit = accountFileLines[i].Split("|");
                        accountDetails.Add(lineSplit[0], lineSplit[1]);
                    }

                    // Read the transactions (if they exist) from line 7 onwards & for each line, create a new
                    // transaction object & stick it in the transactions list
                    List<Transaction> transactions = new List<Transaction>();
                    for (int i = 7; i < Math.Max(7, accountFileLines.Length); i++)
                    {
                        string[] lineSplit = accountFileLines[i].Split("|");

                        // Convert the string transaction type into an enum
                        TransactionType transactionType = TransactionType.Deposit;
                        if (lineSplit[1] == "Withdraw") transactionType = TransactionType.Withdraw;

                        // Create the transaction object based on the data in each line (split by the '|' character)
                        transactions.Add(new Transaction(
                            transactionType,
                            DateTime.ParseExact(lineSplit[0], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                            int.Parse(lineSplit[2]),
                            int.Parse(lineSplit[3])));
                    }

                    // Create a new account object with these details & the respective transactions & add it
                    // to the accounts list
                    accounts.Add(new Account(
                        accountDetails["First Name"],
                        accountDetails["Last Name"],
                        accountDetails["Address"],
                        accountDetails["Email"],
                        int.Parse(accountDetails["Phone"]),
                        int.Parse(accountDetails["AccountNo"]),
                        int.Parse(accountDetails["Balance"]),
                        transactions
                    ));
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed to load account file for {fileDir}");
                    Console.WriteLine(e);
                    Console.ReadKey();
                }
            }

            return accounts;

        }

        /// <summary>
        /// This function looks through the accounts folder & determines the next available free account number
        /// </summary>
        /// <returns>The next free/unused account number</returns>
        public static int GetLatestAccountNumber()
        {
            // Default to 100000 if we don't have any accounts created yet
            int previousID = 100000;

            string[] accountFiles = Directory.GetFiles("accounts");

            // Loop through each file in the accounts directory
            foreach (string accountFile in accountFiles)
            {
                try
                {
                    // Try and parse the file name as a number, if it's larger then the current largest ID
                    // we've found, then update it
                    int accountID = int.Parse(accountFile.Replace("accounts\\", "").Split(".")[0]);
                    if (accountID > previousID)
                    {
                        previousID = accountID;
                    }
                }
                catch { }
            }

            // Return the largest ID we could find plus one
            return previousID + 1;
        }
    }
}