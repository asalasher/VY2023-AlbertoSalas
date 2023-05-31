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

    }
}
