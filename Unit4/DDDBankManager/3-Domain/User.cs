using System;
using System.Collections.Generic;

namespace DDDBankManager
{
    public class User
    {
        public int AccountNumber { get; set; }
        private string Password { get; set; }
        private List<Transaction> Transactions { get; set; }

        public User(int accountNumber, string password)
        {
            AccountNumber = accountNumber;
            Password = password;
        }

        public bool VerifyPassword(string password)
        {
            if (password == Password)
            {
                return true;
            }
            return false;
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
