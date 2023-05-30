using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBankManager
{
    public class AccountManager
    {
        private List<User> Users { get; set; }
        public AccountManager(List<User> users)
        {
            Users = users;
        }

        public User GetUserByAccountNumber(int accountNumber)
        {
            foreach (var user in Users)
            {
                if (user.AccountNumber == accountNumber)
                {
                    return user;
                }
            }
            return null;
        }

        public decimal CalculateBalance(int accountNumber)
        {
            decimal balance = 0.0m;
            foreach (Transaction transaction in Transactions)
            {
                balance += transaction.Quantity;
            }
            return balance;
        }

    }
}
