using System;

namespace AppDevDotNetTask1
{
    public enum TransactionType { Deposit, Withdrawl }

    class Transaction
    {
        private TransactionType type;
        private DateTime time;
        private double amount, balance;

        public Transaction(TransactionType type, DateTime time, double amount, double balance)
        {
            this.type = type;
            this.time = time;
            this.amount = amount;
            this.balance = balance;
        }

        public string TextFileString()
        {
            return string.Format($"{time},{type},${amount},${balance}");
        }

        public override string ToString()
        {
            return string.Format($"║ {time.ToShortDateString()} ║ {time.ToShortTimeString()} ║ {type} ║ ${amount} ║ ${balance} ║");
        }
    }
}
