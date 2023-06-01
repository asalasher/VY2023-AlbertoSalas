using DDDBankManager;
using System.Collections.Generic;

namespace POOBankManagerV2.Classes
{
    public interface IAccount
    {
        int AccountNumber { get; set; }
        List<Transaction> Transactions { get; set; }

        void AddTransaction(decimal quantity);
        decimal GetBalance();
    }
}