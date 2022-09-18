using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AppDevDotNetTask1
{
    class Account
    {
        private string _firstName, _lastName, _address, _email;
        private int _phone;
        private double _balance;
        private List<Transaction> _transactions;

        public readonly int AccountNumber;

        public Account(string firstName, string lastName, string address, string email, int phone, int accountNumber,
                       int balance, List<Transaction> transactions)
        {
            _firstName = firstName;
            _lastName = lastName;
            _address = address;
            _email = email;
            _phone = phone;
            _balance = balance;
            _transactions = transactions;

            AccountNumber = accountNumber;

            SaveToFile();
        }

        /// <summary>
        /// Save the accounts current state to a file
        /// </summary>
        private void SaveToFile()
        {
            try
            {
                // Create a string list with the accounts details, with each element representing a new line in the file
                List<string> accountDetails = new List<string> {
                    $"First Name|{_firstName}",
                    $"Last Name|{_lastName}",
                    $"Address|{_address}",
                    $"Phone|{_phone}",
                    $"Email|{_email}",
                    $"AccountNo|{AccountNumber}",
                    $"Balance|{_balance}"
                };

                // Loop through each transaction & add it to the list so they can be saved
                foreach (Transaction transaction in _transactions)
                {
                    accountDetails.Add(transaction.ToString());
                }

                // Save the list's elements/the account's details to a file with named after it's ID
                File.WriteAllLines($"accounts/{AccountNumber}.txt", accountDetails);
            }
            catch
            {
                Console.WriteLine($"Failed to write account {AccountNumber} to disk.");
            }
        }

        /// <summary>
        /// Prints a window with the accounts details and/or it's statement
        /// </summary>
        /// <param name="isStatement">Whether to change the window title to say statement</param>
        public void PrintAccountDetails(bool isStatement = false)
        {
            // The content to display in the window
            string[] windowFields = new string[] {
                "Account Statement",
                "",
                $"Account No: {AccountNumber}",
                $"Account Balance: ${_balance}",
                $"First Name: {_firstName}",
                $"Last Name: {_lastName}",
                $"Address: {_address}",
                $"Phone: {_phone}",
                $"Email: {_email}"
            };

            if (isStatement)
            {
                Output.PrintWindow("SIMPLE BANKING SYSTEM", "", windowFields);
            }
            else
            {
                // If it isn't a statement, remove the first two lines that contain the statement heading
                Output.PrintWindow("ACCOUNT DETAILS", "", windowFields.Skip(2).ToArray());
            }

        }

        /// <summary>
        /// Attempt to withdraw money from this account
        /// </summary>
        /// <param name="amount">The amount to withdraw</param>
        /// <returns>Whether the withdrawl was successfull</returns>
        public bool Withdraw(double amount)
        {
            // Check the account has a large enough balance to withdraw the amount required
            if (_balance >= amount)
            {
                // Reduce the current balance by the specified amount
                _balance -= amount;

                // Create a new transaction with the relevant details, also delete the oldest
                // transaction if there is more then 5
                _transactions.Add(new Transaction(TransactionType.Withdraw, DateTime.Now, amount, _balance));
                if (_transactions.Count > 5) _transactions.RemoveAt(0);

                // Save the accounts new details, transactions & balance to it's file
                SaveToFile();

                return true;
            }
            return false;
        }

        /// <summary>
        /// Deposit money into this account
        /// </summary>
        /// <param name="amount"></param>
        public void Deposit(double amount)
        {
            // Increase the current balance by the specified amount
            _balance += amount;

            // Create a new transaction with the relevant details, also delete the oldest
            // transaction if there is more then 5.
            _transactions.Add(new Transaction(TransactionType.Deposit, DateTime.Now, amount, _balance));
            if (_transactions.Count > 5) _transactions.RemoveAt(0);

            // Save the accounts new details, transactions & balance to it's file
            SaveToFile();
        }

        /// <summary>
        /// Send the registration email to this account with it's details
        /// </summary>
        /// <returns>Whether the email was sent successfully</returns>
        public bool SendRegistrationEmail()
        {
            return Mailer.SendEmail(MailType.Registration, GenerateEmailParameters());
        }

        /// <summary>
        /// Send an account statement to the accounts email
        /// </summary>
        /// <returns>Whether the email was sent successfully</returns>
        public bool SendStatementEmail()
        {
            return Mailer.SendEmail(MailType.Statement, GenerateEmailParameters());
        }

        /// <summary>
        /// Generate a dictionary with all of the accounts parameters/details in a format
        /// that email template's can accept
        /// </summary>
        /// <returns>A dictionary containing all of the accounts parameters</returns>
        private Dictionary<string, string> GenerateEmailParameters()
        {
            string transactionString = "Past 5 Transactions:<br /><ul>";

            // Loop through each transaction & concatenate them into one long string
            foreach (Transaction transaction in _transactions)
            {
                transactionString += transaction.ToHTML();
            }

            transactionString += "</ul>";

            // If there are no transactions, store an appropriate message
            if (_transactions.Count == 0) transactionString = "This Account Has No Transaction History";

            // Create a dictionary with the relevant key pointing to this accounts details
            // e.g 'FirstName' -> 'Jack'
            return new Dictionary<string, string>() {
                {
                    "FirstName",
                    _firstName
                }, {
                    "LastName",
                    _lastName
                }, {
                    "Balance",
                    _balance.ToString()
                }, {
                    "Phone",
                    _phone.ToString()
                }, {
                    "Address",
                    _address
                }, {
                    "AccountNumber",
                    AccountNumber.ToString()
                }, {
                    "Email",
                    _email
                }, {
                    "Transactions",
                    transactionString
                }
            };
        }
    }
}