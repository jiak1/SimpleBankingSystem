using System;
using System.Collections.Generic;
using System.Text;

namespace AppDevDotNetTask1
{
    class Account
    {
        public readonly string firstName, lastName, address, email;
        public readonly int phone, accountNumber;
        public readonly double balance;
        private List<Transaction> transactions;

        public Account(string firstName, string lastName, string address, string email, int phone, int accountNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.address = address;
            this.email = email;
            this.phone = phone;
            this.accountNumber = accountNumber;
        }

        private int GenerateAccountNumber()
        {
            Random r = new Random();
            return r.Next();
        }

        public bool Withdraw(double amount)
        {
            if(balance >= amount)
            {
                return true;
            }
            return false;
        }

        public void Deposit(double amount)
        {
            
        }
    }
}
