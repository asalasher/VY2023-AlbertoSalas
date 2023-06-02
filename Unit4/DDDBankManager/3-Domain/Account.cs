using DDDBankManager;
using System.Collections.Generic;

namespace POOBankManagerV2.Classes
{
    public class Account
    {
        public int AccountNumber { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account(int accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public Account(int accountNumber, List<Transaction> transactions)
        {
            AccountNumber = accountNumber;
            Transactions = transactions;
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
            return;
        }

        public decimal GetBalance()
        {
            decimal balance = 0.0m;
            if (Transactions.Count == 0)
            {
                return balance;
            }

            foreach (Transaction transaction in Transactions)
            {
                balance += transaction.Quantity;
            }

            return balance;
        }

    }
}
