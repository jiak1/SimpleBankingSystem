using System;

namespace AppDevDotNetTask1
{
    public enum TransactionType
    {
        Deposit,
        Withdraw
    }

    class Transaction
    {
        private TransactionType _type;
        private DateTime _time;
        private double _amount, _balance;

        public Transaction(TransactionType type, DateTime time, double amount, double balance)
        {
            _type = type;
            _time = time;
            _amount = amount;
            _balance = balance;
        }

        /// <summary>
        /// Returns the transaction formatted in HTML in order to send it as part of an email.
        /// </summary>
        /// <returns>The transaction as HTML</returns>
        public string ToHTML()
        {
            return string.Format($"<li>{_time.ToString("dd/MM/yyyy")}, {_type}, ${_amount}, ${_balance}</li>");
        }

        /// <summary>
        /// Converts this transaction object into a string
        /// </summary>
        /// <returns>The transaction object as a string</returns>
        public override string ToString()
        {
            return string.Format($"{_time.ToString("dd.MM.yyyy")}|{_type}|{_amount}|{_balance}");
        }
    }
}