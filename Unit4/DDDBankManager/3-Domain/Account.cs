using DDDBankManager;
using System;
using System.Collections.Generic;

namespace POOBankManagerV2.Classes
{
    public class Account
    {
        private static int totalNumber = 0;
        public int AccountNumber { get; set; }
        public List<Transaction> Transactions { get; set; }

        public Account(string accountNumber)
        {
            totalNumber++;
            AccountNumber = totalNumber;
            Transactions = new List<Transaction>();
        }

        public void AddTransaction(decimal quantity)
        {
            var transaction = new Transaction(quantity);
            Transactions.Add(transaction);
            return;
        }

        public void PrintTransactions(string transactionType)
        {
            if (Transactions.Count == 0)
            {
                Console.WriteLine("There are no transactions to show");
                return;
            }
            foreach (Transaction transaction in Transactions)
            {
                switch (transactionType)
                {
                    case "income":
                        if (transaction.Quantity > 0)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    case "outcome":
                        if (transaction.Quantity < 0)
                        {
                            Console.WriteLine(transaction.ToString());
                        }
                        break;
                    default:
                        Console.WriteLine(transaction.ToString());
                        break;
                }
            }
            return;
        }
    }
}
