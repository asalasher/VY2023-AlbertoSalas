using DDDBankManager;
using System.Collections.Generic;

namespace POOBankManagerV2.Classes
{
    public class Account : IAccount
    {
        public int AccountNumber { get; set; }
        public List<Transaction> Transactions { get; set; }
        public Account(int accountNumber)
        {
            AccountNumber = accountNumber;
            Transactions = new List<Transaction>();
        }
        public Account(int accountNumber, List<Transaction> transactions)
        {
            AccountNumber = accountNumber;
            Transactions = transactions;
        }

        public void AddTransaction(decimal quantity)
        {
            var transaction = new Transaction(quantity);
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

            foreach (Transaction transaction in account.Transactions)
            {
                balance += transaction.Quantity;
            }

            return balance;
        }

    }
}
